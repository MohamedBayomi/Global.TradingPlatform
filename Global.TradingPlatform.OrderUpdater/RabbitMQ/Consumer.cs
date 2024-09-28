using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Global.TradingPlatform.OrderUpdater
{
    public class Consumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Consumer> _logger;
        private readonly TradingPlatformContext _context;
        private IConnection _connection;
        private IModel _channel;

        public Consumer(ILogger<Consumer> logger, IConfiguration configuration, TradingPlatformContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                //UserName = _configuration["RabbitMQ:UserName"],
                //Password = _configuration["RabbitMQ:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => _logger.LogInformation("RabbitMQ background service is stopping."));

            _channel.ExchangeDeclare(exchange: "x_orders", type: ExchangeType.Direct);

            var queueName = _channel.QueueDeclare(
                            queue: "Executions_To_Updater",
                            durable: true,
                            exclusive: false,
                            autoDelete: false).QueueName;

            _channel.QueueBind(queue: queueName,
                              exchange: "x_orders",
                              routingKey: "x_executions");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Execution>(message);
                ProcessMessageAsync(order);
                _logger.LogInformation("Received Order: {0} with Hashcode: {1}", message, message.GetHashCode());
            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            _logger.LogInformation("Listening for messages...");

            return Task.CompletedTask;
        }

        private void ProcessMessageAsync(Execution execution)
        {
            lock (_context)
            {
                var order = _context.Orders.FirstOrDefault(o => o.OrderID == execution.OrderID);
                if (order != null)
                {
                    if (order.Executions == null)
                    {
                        order.Executions = new List<Execution>();
                    }
                    order.Executions.Add(execution);
                    order.ExecutedQuantity = execution.CumulativeQty;
                    order.Status = execution.Status;

                    _context.SaveChanges();
                    _logger.LogInformation("Updated Order: {0} with Executed Quantity: {1}", execution.OrderID, execution.CumulativeQty);
                }
                else
                {
                    _logger.LogWarning("Order with ID: {0} not found", execution.OrderID);
                }
            }
        }
    }
}
