namespace Global.TradingPlatform.OrderService
{
    public class Order : OrderRequest
    {
        public int OrderID { get; set; }
        public string Status { get; set; }
        public int ExecutedQuantity { get; set; }
        public int RemainingQuantity { get; set; }

        public Order(OrderRequest request)
        {
            this.ClordID = request.ClordID;
            this.Quantity = request.Quantity;
            this.Price = request.Price;
            this.Side = request.Side;
            this.Symbol = request.Symbol;
            this.CreatedBy = request.CreatedBy;
            this.RemainingQuantity = request.Quantity;
        }
    }
}
