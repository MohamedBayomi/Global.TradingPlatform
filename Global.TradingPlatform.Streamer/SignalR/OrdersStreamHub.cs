using Microsoft.AspNetCore.SignalR;

namespace Global.TradingPlatform.Streamer
{
    public class OrdersStreamHub : Hub<IOrdersStream>
    {
        public async Task SendOrderToClients(Order dateTime)
        {
            await Clients.All.ReceiveOrderUpdate(dateTime);
        }
    }
}