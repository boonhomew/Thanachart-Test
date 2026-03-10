namespace ThanachartTest.Domain.AggregatesModel.ProductAggregate.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAllProductsAsync();

        Task<ProductModel?> GetProductByIdAsync(Guid id);


    }
}
