using ThanachartTest.Domain.AggregatesModel.ProductAggregate;
using ThanachartTest.Domain.AggregatesModel.ProductAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.StockAggregate.Interfaces;

namespace ThanachartTest.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public ProductService(IProductRepository productRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDtos = new List<ProductModel>();

            foreach (var product in products)
            {
                var stock = await _stockRepository.GetByProductIdAsync(product.Id);
                productDtos.Add(new ProductModel
                {
                    Id = product.Id,
                    ProductSku = product.ProductSku,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    StockQuantity = stock?.Quantity ?? 0
                });
            }

            return productDtos;
        }

        public async Task<ProductModel?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            var stock = await _stockRepository.GetByProductIdAsync(product.Id);
            return new ProductModel
            {
                Id = product.Id,
                ProductSku = product.ProductSku,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                StockQuantity = stock?.Quantity ?? 0
            };
        }
    }
}
