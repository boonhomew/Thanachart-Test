using ThanachartTest.Domain.AggregatesModel.EntityAggregate;

namespace ThanachartTest.Domain.AggregatesModel.StockAggregate.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByProductIdAsync(Guid productId);
        Task UpdateStockAsync(Stock stock);
        Task AddStockAsync(Stock stock);
    }
}
