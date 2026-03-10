using Microsoft.EntityFrameworkCore;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Domain.AggregatesModel.StockAggregate.Interfaces;
using ThanachartTest.Infrastructure.Data;

namespace ThanachartTest.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetByProductIdAsync(Guid productId)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await Task.CompletedTask;
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
        }
    }
}
