namespace Global.TradingPlatform.DesktopApp
{
    internal class OrderRequest
    {
        public Guid ClordID { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CreatedBy { get; set; }
    }
}
