using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.ProductAggregate.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product?> GetProductWithStockAsync(Guid id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
