using System.ComponentModel.DataAnnotations;

namespace Global.TradingPlatform.OrderUpdater
{
    public class Order
    {
        public int OrderID { get; set; }
        public Guid ClordID { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int ExecutedQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public string CreatedBy { get; set; }
    }
}
