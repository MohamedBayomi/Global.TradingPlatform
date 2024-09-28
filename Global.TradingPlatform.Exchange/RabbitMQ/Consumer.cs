using Microsoft.AspNetCore.SignalR;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace Global.TradingPlatform.Exchange
{
    public class Consumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IProducer _producer;
        private readonly ILogger<Consumer> _logger;
        private IConnection _connection;
        private IModel _channel;
        private int executions, delay;

        public Consumer(ILogger<Consumer> logger, IConfiguration configuration, IProducer producer)
        {
            _logger = logger;
            _configuration = configuration;
            _producer = producer;
            
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
                            queue: "Orders_To_Market",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: new Dictionary<string, object>
                            {
                                    { "x-single-active-consumer", true }
                            }).QueueName;

            _channel.QueueBind(queue: queueName,
                              exchange: "x_orders",
                              routingKey: "x_orders");

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

        private void Execute(Order order)
        {
            var strexecutions = _configuration["settings:executions"];
            int.TryParse(strexecutions, out executions);
            var strDelay = _configuration["settings:delay"];
            int.TryParse(strDelay, out delay);

            var e = new Execution { 
                OrderID = order.OrderID,
                OrderQty = order.Quantity,
                LastShares = 0,
                CumulativeQty = 0,
                LeavesQuantity = order.Quantity,
                OperationNumber = Guid.NewGuid(),
                Status = "Accepted",
                OperationTime = DateTime.UtcNow
            };
            _producer.SendOrder(e);
            Task.Delay(delay).Wait();
            order.ExecutedQuantity = 0;
            order.RemainingQuantity = order.Quantity;

            if (order.Quantity < executions)
                executions = order.Quantity;
            
            int delta = order.Quantity / executions;
            int error = order.Quantity - delta * executions;
            int lastShares = 0;
            for (int i = 1; i < executions; i++)
            {
                lastShares = delta + ((error-- > 0) ? 1 : 0);
                order.ExecutedQuantity += lastShares;
                order.RemainingQuantity = order.Quantity - order.ExecutedQuantity;
                e = new Execution
                {
                    OrderID = order.OrderID,
                    OrderQty = order.Quantity,
                    LastShares = lastShares,
                    CumulativeQty = order.ExecutedQuantity,
                    LeavesQuantity = order.RemainingQuantity,
                    OperationNumber = Guid.NewGuid(),
                    Status = "PartiallyFilled",
                    OperationTime = DateTime.UtcNow
                };
                _producer.SendOrder(e);
                Task.Delay(delay).Wait();
            }
            
            lastShares = order.Quantity - order.ExecutedQuantity;
            order.ExecutedQuantity = order.Quantity;
            order.RemainingQuantity = 0;
            e = new Execution
            {
                OrderID = order.OrderID,
                OrderQty = order.Quantity,
                LastShares = lastShares,
                CumulativeQty = order.ExecutedQuantity,
                LeavesQuantity = order.RemainingQuantity,
                OperationNumber = Guid.NewGuid(),
                Status = "Filled",
                OperationTime = DateTime.UtcNow
            };
            _producer.SendOrder(e);
            Task.Delay(delay).Wait();
        }

        private Task ProcessMessageAsync(string message)
        {
            return Task.Run(() =>
            {
                var order = JsonSerializer.Deserialize<Order>(message);
                Execute(order);
                _logger.LogInformation(" [x] Received {0} {1}", order.OrderID, order.CreatedBy);
            });
        }
    }
}