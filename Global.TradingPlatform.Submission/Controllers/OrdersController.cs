using Microsoft.AspNetCore.Mvc;

namespace Global.TradingPlatform.Submission
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IProducer producer;

        public OrdersController(IOrdersRepository ordersRepository, IProducer producer)
        {
            this.ordersRepository = ordersRepository;
            this.producer = producer;
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

            var newOrder = ordersRepository.Create(request);
            newOrder.Status = "PendingNew";

            // Send the order to RabbitMQ
            producer.SendOrder(newOrder);

            return CreatedAtAction(nameof(GetOrders), new { Clordid = request.ClordID }, newOrder);
        }
    }
}
