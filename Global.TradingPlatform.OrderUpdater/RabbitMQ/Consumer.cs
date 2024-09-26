using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace Global.TradingPlatform.OrderUpdater
{
    public class Consumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Consumer> _logger;
        private IConnection _connection;
        private IModel _channel;

        public Consumer(ILogger<Consumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Order>(message);
                ProcessMessageAsync(message);
                _logger.LogInformation("Received Order: {0} with Hashcode: {1}", message, message.GetHashCode());
            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            _logger.LogInformation("Listening for messages...");

            return Task.CompletedTask;
        }

        private Task ProcessMessageAsync(string message)
        {
            return Task.Run(() =>
            {
                var order = JsonSerializer.Deserialize<Order>(message);
                _logger.LogInformation(" [x] Received {0} {1}", order.OrderID, order.CreatedBy);
            });
        }
    }
}