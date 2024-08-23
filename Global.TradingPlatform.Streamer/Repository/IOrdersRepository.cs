namespace Global.TradingPlatform.Streamer
{
    public interface IOrdersRepository
    {
        Order Create(Order order);
        Task<List<Order>> Get();
    }
}
