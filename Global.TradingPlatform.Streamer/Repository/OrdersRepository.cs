namespace Global.TradingPlatform.Streamer
{
    public class OrdersRepository_InMemoryCache : IOrdersRepository
    {
        private static readonly Dictionary<Guid, Order> _ordersCache = new();

        public Order Add(Order order)
        {
            _ordersCache[order.ClordID] = order;
            return order;
        }

        public async Task<List<Order>> GetAll()
        {
            return _ordersCache.Values.ToList();
        }
    }
}
