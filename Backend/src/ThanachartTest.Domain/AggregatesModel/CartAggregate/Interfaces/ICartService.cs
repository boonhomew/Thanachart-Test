
namespace ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces
{
    public interface ICartService
    {
        Task<CartModel> GetActiveCartAsync();

        Task<CartModel> AddToCartAsync(AddToCartRequest request);

        Task<CartModel> UpdateCartItemAsync(UpdateCartItemRequest request);

        Task<CartModel> RemoveCartItemAsync(Guid cartItemId);

        Task<CartModel> ClearCartAsync();
    }
}
