using Microsoft.AspNetCore.Mvc;

namespace Global.TradingPlatform.OrderService
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IProducer producer;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IOrdersRepository ordersRepository, IProducer producer, ILogger<OrdersController> logger)
        {
            this.ordersRepository = ordersRepository;
            this.producer = producer;
            this.logger = logger;
        }

        [HttpGet()]
        public IEnumerable<Order> GetOrders()
        {
            return ordersRepository.Get();
        }

        [HttpGet("{ClordID}")]
        public async Task<ActionResult<Order>> GetOrders(Guid ClordID)
        {
            return ordersRepository.Get(ClordID);
        }

        [HttpPost("submit")]
        public async Task<ActionResult<Order>> PostClients([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            logger.LogInformation(">>> Received Order: {0} with Hashcode: {1}", request, request.GetHashCode());
            var newOrder = ordersRepository.Create(request);
            newOrder.Status = "PendingNew";

            logger.LogInformation(">>> Order to RMQ: {0} with Hashcode: {1}", newOrder, newOrder.GetHashCode());
            // Send the order to RabbitMQ
            producer.SendOrder(newOrder);

            return CreatedAtAction(nameof(GetOrders), new { Clordid = request.ClordID }, newOrder);
        }
    }
}
