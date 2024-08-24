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

            //_channel.QueueDeclare(
            //    queue: "queue1", 
            //    durable: false, 
            //    exclusive: false, 
            //    autoDelete: false, 
            //    arguments: null);


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

            _channel.ExchangeDeclare(exchange: "Order_logs", type: ExchangeType.Fanout);

            var queueName = _channel.QueueDeclare(
                            queue: "queue1",           // Leave the queue name empty to let RabbitMQ generate a unique name, or specify your own name.
                            durable: true,      // Set to true if you want the queue to survive a broker restart.
                            exclusive: false,    // Set to true if the queue is only used by one connection and will be deleted when that connection closes.
                            autoDelete: false,   // Set to false to prevent the queue from being automatically deleted when the last consumer unsubscribes.
                            arguments: new Dictionary<string, object>
                            {
                                { "x-single-active-consumer", true }  // Enable Single Active Consumer
                            }).QueueName;

            _channel.QueueBind(queue: queueName,
                              exchange: "Order_logs",
                              routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var order = JsonSerializer.Deserialize<Order>(message);
                ProcessMessageAsync(message);
                _logger.LogInformation("Received Order: {0}", message);

                // Here you can do further processing, like saving to a database
            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            _logger.LogInformation("Listening for messages...");

            // Keep the connection alive
            //while (_connection.IsOpen)
            //{
            //    Task.Delay(1000, stoppingToken);
            //}
            return Task.CompletedTask;
        }


        private async Task PublishOrderStream(Order order)
        {
            await _ordersStreamHub.Clients.All.ReceiveOrderUpdate(order);
        }

        private Task ProcessMessageAsync(string message)
        {
            // Simulate some asynchronous work
            return Task.Run(() =>
            {
                var order = JsonSerializer.Deserialize<Order>(message);
                ordersRepository.Add(order);
                PublishOrderStream(order);
                // Add your message processing logic here
                _logger.LogInformation(" [x] Received {0} {1}", order.OrderID, order.CreatedBy);
            });
        }

    }
}