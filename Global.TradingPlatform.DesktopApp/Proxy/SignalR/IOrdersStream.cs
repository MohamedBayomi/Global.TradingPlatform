namespace Global.TradingPlatform.DesktopApp
{
    public interface IOrdersStream
    {
        Task ReceiveOrderUpdate(Order order);
    }
}