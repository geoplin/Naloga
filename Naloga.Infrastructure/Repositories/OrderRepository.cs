using Microsoft.EntityFrameworkCore;
using Naloga.Aplication.Interfaces;
using Naloga.Domain.Entities;
using Naloga.Infrastructure.Data;

namespace Naloga.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(AppDbContext appDbContext, ILogger<OrderRepository> logger)
        {
            this._context = appDbContext;
            this._logger = logger;
        }

        public async Task AddOrder(Order order)
        {
            _logger.LogInformation($"Adding order with ID {order.OrderId}.");
            var duplicate = _context.Orders.Find(order.OrderId);
            if (duplicate == null)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Order with ID {order.OrderId} already exists. Skipping addition.");
            }
        }

        public async Task DeleteOrder(int id)
        {
            _logger.LogInformation($"Deleting order with ID {id}.");
            var order = _context.Orders.Find(id);
            if (order != null)
            {

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Order with ID {id} not found. Skipping deletion.");
            }
        }

        public async Task<Order> GetOrder(int id)
        {
            _logger.LogInformation($"Retrieving order with ID {id}.");
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrders()
        {
            _logger.LogInformation($"Retrieving all orders.");
            return await _context.Orders.ToListAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _logger.LogInformation($"Updating order with ID {order.OrderId}.");
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
