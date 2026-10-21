using Naloga.Domain.Entities;

namespace Naloga.Aplication.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetOrders();
        Task AddOrder(Order order);
        Task UpdateOrder(Order order);
        Task DeleteOrder(int id);
    }
}
