namespace Global.TradingPlatform.Streamer
{
    public class OrdersRepository_InMemoryCache : IOrdersRepository
    {
        private static readonly Dictionary<Guid, Order> _ordersCache = new();

        public Order Create(Order order)
        {
            return order;
        }

        public async Task<List<Order>> Get()
        {
            return _ordersCache.Values.ToList();
        }
    }
}
