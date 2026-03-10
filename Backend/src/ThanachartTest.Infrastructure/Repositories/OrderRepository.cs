using Microsoft.EntityFrameworkCore;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Domain.AggregatesModel.OrderAggregate.Interfaces;
using ThanachartTest.Infrastructure.Data;

namespace ThanachartTest.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await Task.CompletedTask;
        }
    }
}
