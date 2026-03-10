using Microsoft.EntityFrameworkCore;
using ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Infrastructure.Data;

namespace ThanachartTest.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetActiveCartAsync()
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Status == "ACTIVE");
        }

        public async Task<Cart?> GetCartByIdWithItemsAsync(Guid cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await Task.CompletedTask;
        }
    }
}
