using Retro.ResultWrappers;
using Retro.Stock.Contracts.Request;
using Retro.Stock.Contracts.Response;
using Retro.Stock.Domain;

namespace Retro.Stock.Infrastructure;

public class StockService(IStockRepository repository) : IStockService
{
    //Mappers
    private PagedStockResponse PagedStockResponseMapper(PaginatedResult<StockItem> items)
    {
        return new PagedStockResponse(
            Items: items.Items.Select(StockResponseMapper).AsEnumerable(),
            PageNumber: items.PageNumber,
            PageSize: items.PageSize,
            TotalCount: items.TotalCount
        );
    }

    private StockResponse StockResponseMapper(StockItem item)
    {
        return new StockResponse(
            Id: item.Id,
            Sku: item.Sku,
            Name: item.Title,
            Description: item.Description,
            Price: item.Price,
            Quantity: item.Quantity,
            DiscountPercentage: item.DiscountPercentage,
            IsDiscounted: item.IsDiscounted,
            Tags: item.Tags,
            ImageUrl: item.ImageUrl
        );
    }

    private StockItem StockItemMapper(UpsertStockRequest request)
    {
        return new StockItem
        {
            Id = request.Id ?? Guid.Empty,
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            DiscountPercentage = request.DiscountPercentage,
            IsDiscounted = request.IsDiscounted,
            Tags = request.Tags,
            Sku = request.Sku,
            Condition = request.Condition,
            ImageUrl = request.ImageUrl,
            CreatedAt = request.CreatedAt,
            UpdatedAt = request.UpdatedAt,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };
    }
    //Mappers END

    public async Task<PagedStockResponse> GetAllAsync(GetAllByPageRequest request,
        CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return PagedStockResponseMapper(items);
    }

    public async Task<StockResponse> GetByIdAsync(GetByIdRequest request, CancellationToken cancellationToken = default)
    {
        var item = await repository.GetByIdAsync(request.Id, cancellationToken);
        return StockResponseMapper(item);
    }

    public async Task<StockResponse> GetBySkuAsync(GetBySkuRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await repository.GetBySkuAsync(request.Sku, cancellationToken);
        return StockResponseMapper(item);
    }

    public async Task UpsertStock(UpsertStockRequest request, CancellationToken cancellationToken = default)
    {
        var item = StockItemMapper(request);
        
        //if Item is new Create
        if (item.Id == Guid.Empty)
        {
            await repository.AddAsync(item, cancellationToken);
            return;
        }
        
        await repository.UpdateAsync(item, cancellationToken);
    }

    public async Task DeleteAsync(DeleteRequest request, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(request.Sku))
        {
            await repository.DeleteAsync(request.Sku, cancellationToken);
        }
        else
        {
            await repository.DeleteAsync(request.Id.Value, cancellationToken);
        }
    }

    public async Task<PagedStockResponse> GetByConditionAsync(GetByConditionRequest request,
        CancellationToken cancellationToken = default)
    {
        var items = await repository.GetByConditionAsync(request.PageNumber, request.PageSize, request.Condition,
            cancellationToken);
        return PagedStockResponseMapper(items);
    }

    public async Task<PagedStockResponse> GetByTitleAsync(GetByTitleRequest request,
        CancellationToken cancellationToken = default)
    {
        var items = await repository.GetByTitleAsync(request.Title, request.PageNumber, request.PageSize,
            cancellationToken);
        return PagedStockResponseMapper(items);
    }
}