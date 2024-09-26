namespace Global.TradingPlatform.OrderService
{
    public interface IOrdersRepository
    {
        Order Create(OrderRequest order);
        List<Order> Get();
        Order Get(Guid Clordid);
    }
}
