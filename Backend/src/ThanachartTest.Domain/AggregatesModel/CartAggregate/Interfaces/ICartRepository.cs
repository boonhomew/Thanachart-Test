using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetActiveCartAsync();
        Task<Cart?> GetCartByIdWithItemsAsync(Guid cartId);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
    }
}
