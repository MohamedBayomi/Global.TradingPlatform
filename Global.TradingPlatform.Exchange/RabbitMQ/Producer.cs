using RabbitMQ.Client;
using System.Text;

namespace Global.TradingPlatform.Exchange
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
                channel.ExchangeDeclare("x_orders", ExchangeType.Direct);

                string message = System.Text.Json.JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "x_orders",
                                     routingKey: "x_executions",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}