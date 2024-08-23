using Microsoft.AspNetCore.SignalR.Client;

namespace Global.TradingPlatform.DesktopApp
{
    internal static class StreamerServiceProxy
    {
        private static HubConnection _hubConnection;
        private static List<Order> orders = new List<Order>();

        public static void Subscribe()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/streaming")
                .Build();

            // Set up event handler for receiving messages
            _hubConnection.On<Order>("ReceiveOrderUpdate", orders.Add);

            try
            {
                // Start the connection
                _hubConnection.StartAsync().Wait();
                MessageBox.Show("Connected to SignalR hub.\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to SignalR hub: {ex.Message}\n");
            }
        }

        public static List<Order> GetSnapshotOrders()
        {
            return orders;
        }
    }
}
