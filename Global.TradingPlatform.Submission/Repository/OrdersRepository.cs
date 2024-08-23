namespace Global.TradingPlatform.Submission
{
    public class OrdersRepository_InMemoryCache : IOrdersRepository
    {
        private static readonly Dictionary<Guid, Order> _ordersCache = new();

        public Order Create(OrderRequest request)
        {
            var order = new Order(request);
            _ordersCache[request.ClordID] = order;
            order.OrderID = _ordersCache.Count;

            return order;
        }

        public List<Order> Get()
        {
            return _ordersCache.Values.ToList();
        }

        public Order Get(Guid ClordID)
        {
            if (_ordersCache.TryGetValue(ClordID, out var order))
            {
                return order;
            }

            return null;
        }
    }
}
