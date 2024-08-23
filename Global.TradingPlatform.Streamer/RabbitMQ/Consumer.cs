using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Text.Json;

namespace Global.TradingPlatform.Streamer
{
    public class Consumer : IConsumer
    {
        private readonly IConfiguration _configuration;

        public Consumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void StartListening()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                //UserName = _configuration["RabbitMQ:UserName"],
                //Password = _configuration["RabbitMQ:Password"]
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: "queue1",
                //    durable: false,
                //    exclusive: false,
                //    autoDelete: false,
                //    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    
                    var order = JsonSerializer.Deserialize<Order>(message);

                    Console.WriteLine(" [x] Received {0} {1}", order.OrderID, order.CreatedBy);
                };
                channel.BasicConsume(queue: "queue1",
                    autoAck: true,
                    consumer: consumer);

                while(true)
                {
                    // Keep the consumer running
                    Thread.Sleep(1000);
                }
            }
        }
    }
}