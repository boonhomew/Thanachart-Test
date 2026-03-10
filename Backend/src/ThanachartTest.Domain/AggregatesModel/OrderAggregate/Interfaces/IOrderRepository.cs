using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.OrderAggregate.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
    }
}
