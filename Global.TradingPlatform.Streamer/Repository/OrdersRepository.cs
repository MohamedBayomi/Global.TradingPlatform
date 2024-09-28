namespace Global.TradingPlatform.Streamer
{
    public class OrdersRepository_InMemoryCache : IOrdersRepository
    {
        private static readonly Dictionary<int, Order> _ordersCache = new();

        public Order Add(Order order)
        {
            _ordersCache[order.OrderID] = order;
            return order;
        }

        public async Task<List<Order>> GetAll()
        {
            return _ordersCache.Values.ToList();
        }

        public Order GetOrderByID(int ID)
        {
            return _ordersCache.GetValueOrDefault(ID);
        }
    }
}
