namespace Global.TradingPlatform.DesktopApp
{
    internal static class StreamerServiceProxy
    {
        public static List<OrderRequest> GetSnapshotOrders()
        {
            return new List<OrderRequest>
            {
                new OrderRequest
                {
                    ClordID = Guid.NewGuid(),
                    CreatedBy = "mohamed",
                    ExecutedQuantity = 0,
                    OrderID = 1,
                    Price = 100,
                    Quantity = 100,
                    RemainingQuantity = 100,
                    Side = "Buy",
                    Status = "New",
                    Symbol = "AAPL"
                },
                new OrderRequest
                {
                    ClordID = Guid.NewGuid(),
                    CreatedBy = "ahmed",
                    ExecutedQuantity = 0,
                    OrderID = 2,
                    Price = 200,
                    Quantity = 200,
                    RemainingQuantity = 200,
                    Side = "Sell",
                    Status = "New",
                    Symbol = "MSFT"
                }
            };
        }

        internal static void Subscribe()
        {
            
        }
    }
}
