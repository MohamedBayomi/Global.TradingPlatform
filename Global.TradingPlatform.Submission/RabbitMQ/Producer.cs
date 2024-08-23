using RabbitMQ.Client;
using System.Text;

namespace Global.TradingPlatform.Submission
{
    public class Producer : IProducer
    {
        private readonly IConfiguration _configuration;

        public Producer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendOrder(Order order)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                //Port = 5671,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("Order_logs", ExchangeType.Fanout);
                /*channel.QueueDeclare(queue: "order_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                */

                string message = System.Text.Json.JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "Order_logs", //default exchange "",messages are routed to the queue with the name specified by routingKey
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}