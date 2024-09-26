using Microsoft.AspNetCore.SignalR.Client;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var _connection = new HubConnectionBuilder()
                .WithUrl("" + "/hubs/ordersstream")
                .Build();

            _connection.On<Order>("ReceiveOrderUpdate", ReceiveOrderUpdate);
            _connection.StartAsync();
        }
    }
}
