using Microsoft.AspNetCore.Mvc;
using Naloga.Aplication.Interfaces;
using Naloga.Domain.Entities;

namespace Naloga.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this._orderService = orderService;
            this._logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                _logger.LogWarning($"Order with ID {id} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Order with ID {id} retrieved successfully.");
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrders()
        {
            var orders = await _orderService.GetOrders();
            _logger.LogInformation($"Retrieved {orders.Count} orders.");
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderService.AddOrder(order);
            _logger.LogInformation($"Order with ID {order.OrderId} created successfully.");
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderId)
            {
                _logger.LogWarning($"Order ID mismatch: {id} does not match {order.OrderId}.");
                return BadRequest();
            }

            await _orderService.UpdateOrder(order);
            _logger.LogInformation($"Order with ID {id} updated successfully.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            _logger.LogInformation($"Order with ID {id} deleted successfully.");
            return NoContent();
        }
    }
}
