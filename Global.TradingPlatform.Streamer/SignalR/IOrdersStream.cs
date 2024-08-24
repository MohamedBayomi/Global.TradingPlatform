namespace Global.TradingPlatform.Streamer
{
    public interface IOrdersStream
    {
        Task ReceiveOrderUpdate(Order order);
    }
}