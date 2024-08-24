using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

using System.Net.Http.Json;

namespace Global.TradingPlatform.DesktopApp
{
    public partial class OrdersHubClient : IOrdersStream
    {
        public event EventHandler<Order> OnOrderUpdate;
        private static List<Order> orders = new List<Order>();
        private HubConnection _connection;
        private string _StreamingUrl;
        public OrdersHubClient()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _StreamingUrl = configuration["ApiSettings:Streaming"];

            _connection = new HubConnectionBuilder()
                .WithUrl(_StreamingUrl + "/hubs/ordersstream")
                .Build();

            _connection.On<Order>("ReceiveOrderUpdate", ReceiveOrderUpdate);
            _connection.StartAsync();
        }

        public Task ReceiveOrderUpdate(Order order)
        {
            Merge(order);
            if (OnOrderUpdate != null)
            {
                OnOrderUpdate(this, order);
            }
            return Task.CompletedTask;
        }

        public List<Order> GetAllOrders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_StreamingUrl);

                // Use asynchronous methods for HTTP requests
                var response = client.GetAsync("orders");
                Console.WriteLine($"Response Status: {response.Status}");
                var responseContent = response.Result.Content;
                Console.WriteLine($"Response Content: {responseContent}");


                // Deserialize the response content into an Order object
                var result = response.Result.Content.ReadFromJsonAsync<List<Order>>().Result;

                Merge(result);

                return orders;
            }
        }

        private void Merge(List<Order> result)
        {
            foreach (var y in result)
            {
                Merge(y);
            }
        }

        private static void Merge(Order y)
        {
            var x = orders.FirstOrDefault(o => o.ClordID == y.ClordID);
            if (x == null)
            {
                orders.Add(y);
            }
            else
            {
                //x.ClordID = y.ClordID;
                x.OrderID = y.OrderID;
                x.Symbol = y.Symbol;
                x.Side = y.Side;
                x.Quantity = y.Quantity;
                x.Price = y.Price;
                x.CreatedBy = y.CreatedBy;
                x.Status = y.Status;
                x.ExecutedQuantity = y.ExecutedQuantity;
                x.RemainingQuantity = y.RemainingQuantity;
                x.CreatedBy = y.CreatedBy;
                x.Status = y.Status;
            }
        }

        public void Dispose()
        {
            _connection.DisposeAsync();
        }
    }
}
