namespace Retro.Stock.Contracts.Response;

public record PagedStockResponse(IEnumerable<StockResponse> Items, int TotalCount, int PageNumber, int PageSize);