﻿using RabbitMQ.Client;
using System.Text;

namespace Global.TradingPlatform.OrderService
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
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                VirtualHost = _configuration["RabbitMQ:VirtualHost"],
                //Port = 5671,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("x_orders", ExchangeType.Direct);
                
                string message = System.Text.Json.JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>
                {
                    { "MessageType", "x_order" }
                };

                channel.BasicPublish(exchange: "x_orders", //default exchange "",messages are routed to the queue with the name specified by routingKey
                                     routingKey: "x_orders",
                                     basicProperties: properties,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}