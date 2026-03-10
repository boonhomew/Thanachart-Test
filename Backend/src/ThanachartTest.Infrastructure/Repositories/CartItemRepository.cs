using Microsoft.EntityFrameworkCore;
using ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Infrastructure.Data;

namespace ThanachartTest.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
        }

        public async Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await Task.CompletedTask;
        }

        public async Task DeleteCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
            await Task.CompletedTask;
        }
    }
}
