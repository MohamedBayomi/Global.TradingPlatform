using Microsoft.AspNetCore.Mvc;

namespace Global.TradingPlatform.Streamer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersRepository ordersRepository;

        public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository)
        {
            _logger = logger;
            this.ordersRepository = ordersRepository;
        }

        [HttpGet()]
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await ordersRepository.GetAll();
        }
    }
}
