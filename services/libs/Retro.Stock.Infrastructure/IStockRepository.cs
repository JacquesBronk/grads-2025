using Retro.ResultWrappers;
using Retro.Stock.Domain;

namespace Retro.Stock.Infrastructure;

public interface IStockRepository
{
    Task<PaginatedResult<StockItem>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<StockItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StockItem> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task AddAsync(StockItem stockItem, CancellationToken cancellationToken = default);
    Task UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string sku, CancellationToken cancellationToken = default);
    Task<PaginatedResult<StockItem>> GetByConditionAsync(int pageNumber, int pageSize,StockCondition condition, CancellationToken cancellationToken = default);
    Task<PaginatedResult<StockItem>> GetByTitleAsync(string title,int pageNumber, int pageSize, CancellationToken cancellationToken = default);
   
}