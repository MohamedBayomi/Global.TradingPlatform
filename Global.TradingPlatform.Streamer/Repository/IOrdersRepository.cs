namespace Global.TradingPlatform.Streamer
{
    public interface IOrdersRepository
    {
        Order Add(Order order);
        Task<List<Order>> GetAll();
    }
}
