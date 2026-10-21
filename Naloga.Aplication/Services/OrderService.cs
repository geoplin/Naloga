using Naloga.Aplication.Interfaces;
using Naloga.Domain.Entities;

namespace Naloga.Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            this._orderRepository = orderRepository;
            this._logger = logger;
        }

        public async Task AddOrder(Order order)
        {
            _logger.LogInformation($"Adding order with ID {order.OrderId}.");
            await _orderRepository.AddOrder(order);
        }

        public async Task DeleteOrder(int id)
        {
            _logger.LogInformation($"Deleting order with ID {id}.");
            await _orderRepository.DeleteOrder(id);
        }

        public async Task<Order> GetOrder(int id)
        {
            _logger.LogInformation($"Retrieving order with ID {id}.");
            return await _orderRepository.GetOrder(id);
        }

        public async Task<List<Order>> GetOrders()
        {
            _logger.LogInformation($"Retrieving all orders.");
            return await _orderRepository.GetOrders();
        }

        public async Task UpdateOrder(Order order)
        {
            _logger.LogInformation($"Updating order with ID {order.OrderId}.");
            await _orderRepository.UpdateOrder(order);
        }
    }
}
