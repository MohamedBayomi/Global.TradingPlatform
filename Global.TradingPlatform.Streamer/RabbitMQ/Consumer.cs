using Microsoft.AspNetCore.SignalR;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Data.Common;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Global.TradingPlatform.Streamer
{
    public class Consumer : BackgroundService
    {
        private bool IsListening = false;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Consumer> _logger;
        private IConnection _connection;
        private IModel _channel;
        private IOrdersRepository ordersRepository;
        private readonly IHubContext<OrdersStreamHub, IOrdersStream> _ordersStreamHub;

        public Consumer(ILogger<Consumer> logger, IConfiguration configuration, IOrdersRepository ordersRepository, IHubContext<OrdersStreamHub, IOrdersStream> _ordersStreamHub)
        {
            _logger = logger;
            this._configuration = configuration;
            this._ordersStreamHub = _ordersStreamHub;
            this.ordersRepository = ordersRepository;
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
                            queue: "Orders_To_Streamer",
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

            _channel.QueueBind(queue: queueName,
                              exchange: "x_orders",
                              routingKey: "x_executions");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Read the header
                if (ea.BasicProperties.Headers != null && ea.BasicProperties.Headers.TryGetValue("MessageType", out var messageTypeObj))
                {
                    var messageType = Encoding.UTF8.GetString((byte[])messageTypeObj);
                    _logger.LogInformation("Received message with type: {0}", messageType);

                    if (messageType == "x_execution")
                    {
                        var execution = JsonSerializer.Deserialize<Execution>(message);
                        ProcessExecutionAsync(execution);
                    }
                    else
                    {
                        var order = JsonSerializer.Deserialize<Order>(message);
                        ProcessOrderAsync(order);
                    }
                }
                else
                {
                    _logger.LogWarning("Message received without MessageType header.");
                }

                _logger.LogInformation("Received Order: {0} with Hashcode: {1}", message, message.GetHashCode());
            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            _logger.LogInformation("Listening for messages...");

            return Task.CompletedTask;
        }

        private async Task PublishOrderStream(Order order)
        {
            await _ordersStreamHub.Clients.All.ReceiveOrderUpdate(order);
        }

        private Task ProcessOrderAsync(Order order)
        {
            return Task.Run(() =>
            {
                ordersRepository.Add(order);
                PublishOrderStream(order);
                _logger.LogInformation(" [x] Received Order {0} {1}", order.OrderID, order.CreatedBy);
            });
        }

        private Task ProcessExecutionAsync(Execution execution)
        {
            return Task.Run(() =>
            {
                var order = ordersRepository.GetOrderByID(execution.OrderID);
                if (order != null)
                {
                    order.ExecutedQuantity = execution.CumulativeQty;
                    order.RemainingQuantity = execution.LeavesQuantity;
                    order.Status = execution.Status;
                    PublishOrderStream(order);
                    _logger.LogInformation(" [x] Received Execution {0} {1}", execution.ExecutionID, execution.CumulativeQty);
                }
                else
                {
                    _logger.LogWarning("Order not found for Execution {0}", execution.ExecutionID);
                }
            });
        }
    }
}