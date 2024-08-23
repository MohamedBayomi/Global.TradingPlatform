namespace Global.TradingPlatform.Submission
{
    public interface IProducer
    {
        void SendOrder(Order order);
    }
}