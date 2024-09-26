namespace Global.TradingPlatform.Exchange
{
    public interface IProducer
    {
        void SendOrder(Order order);
    }
}