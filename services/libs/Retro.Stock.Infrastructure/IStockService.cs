using Retro.Stock.Contracts.Request;
using Retro.Stock.Contracts.Response;
using Retro.Stock.Domain;

namespace Retro.Stock.Infrastructure;

public interface IStockService
{
    Task<PagedStockResponse> GetAllAsync(GetAllByPageRequest request, CancellationToken cancellationToken = default);
    Task<StockResponse> GetByIdAsync(GetByIdRequest request, CancellationToken cancellationToken = default);
    Task<StockResponse> GetBySkuAsync(GetBySkuRequest request, CancellationToken cancellationToken = default);
    Task UpsertStock(UpsertStockRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(DeleteRequest request, CancellationToken cancellationToken = default);
    Task<PagedStockResponse> GetByConditionAsync(GetByConditionRequest request, CancellationToken cancellationToken = default);
    Task<PagedStockResponse> GetByTitleAsync(GetByTitleRequest request, CancellationToken cancellationToken = default);
}