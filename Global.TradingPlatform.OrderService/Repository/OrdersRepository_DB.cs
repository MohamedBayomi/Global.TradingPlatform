namespace Global.TradingPlatform.OrderService
{
    public class OrdersRepository_DB : IOrdersRepository
    {
        private readonly TradingPlatformContext _context;

        public OrdersRepository_DB(TradingPlatformContext context)
        {
            _context = context;
        }

        public Order Create(OrderRequest request)
        {
            var order = new Order(request);
            
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public List<Order> Get()
        {
            return _context.Orders.ToList();
        }

        public Order Get(Guid ClordID)
        {
            return _context.Orders.FirstOrDefault(o => o.ClordID == ClordID);
        }
    }
}
