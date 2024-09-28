namespace Global.TradingPlatform.Streamer
{
    public interface IOrdersRepository
    {
        Order Add(Order order);
        Order GetOrderByID(int ID);
        Task<List<Order>> GetAll();
    }
}
