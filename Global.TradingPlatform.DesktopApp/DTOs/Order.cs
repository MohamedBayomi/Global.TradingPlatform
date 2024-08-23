using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.TradingPlatform.DesktopApp
{
    internal class Order : OrderRequest
    {
        public int OrderID { get; set; }
        public string Status { get; set; }
        public int ExecutedQuantity { get; set; }
        public int RemainingQuantity { get; set; }
    }
}
