namespace Global.TradingPlatform.OrderService
{
    public interface IProducer
    {
        void SendOrder(Order order);
    }
}