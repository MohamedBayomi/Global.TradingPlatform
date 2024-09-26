using Microsoft.AspNetCore.Mvc;

namespace Global.TradingPlatform.OrderUpdater.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public ActionResult GetOrders()
        {
            return Ok("ok");
        }
    }
}
