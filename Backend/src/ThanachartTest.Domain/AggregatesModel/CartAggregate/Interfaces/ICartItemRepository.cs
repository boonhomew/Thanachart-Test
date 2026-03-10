using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteCartItemAsync(CartItem cartItem);
    }
}
