using System.Net;
using System.Text;
using System.Text.Json;
using Retro.Http;
using Retro.Stock.Contracts.Request;
using Retro.Stock.Contracts.Response;
using Retro.Stock.Domain;

namespace E2ERetroShop.Util;

public class StockGateway
{
    private readonly string _baseUrl = "http://localhost:5000/stock-api/stock";
    
    public async Task<PagedStockResponse?> GetStockItemsAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}?page={page}&pageSize={pageSize}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<PagedStockResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<StockResponse?> GetStockItemByIdAsync(Guid stockItemId, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/{stockItemId.ToString()}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<StockResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<StockResponse?> GetStockItemBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/sku/{sku}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<StockResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<HttpStatusCode> CreateStockItemAsync(UpsertStockRequest stockItem, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .WithContent(new StringContent(JsonSerializer.Serialize(stockItem), Encoding.UTF8, "application/json"))
            .EnsureStatusCode(HttpStatusCode.NoContent)
            .Build().GetStatusCodeAsync(cancellationToken);

        return HttpStatusCode.NoContent;
    }
    
    public async Task<HttpStatusCode> UpdateStockItemAsync(UpsertStockRequest stockItem, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .WithContent(new StringContent(JsonSerializer.Serialize(stockItem), Encoding.UTF8, "application/json"))
            .EnsureStatusCode(HttpStatusCode.NoContent)
            .Build()
            .GetStatusCodeAsync(cancellationToken);

        return HttpStatusCode.NoContent;
    }
    
    public async Task<HttpStatusCode> DeleteStockItemAsync(Guid stockItemId, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/{stockItemId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Delete)
            .EnsureStatusCode(HttpStatusCode.NoContent)
            .Build()
            .GetStringResultAsync(cancellationToken);

        return HttpStatusCode.NoContent;
    }
    
    public async Task<PagedStockResponse> GetStockByCondition(int page, int pageSize,StockCondition condition, CancellationToken cancellationToken)
    {
        int stockCondition = (int)condition;
        var stockItemUrl = $"{_baseUrl}/search/condition?page={page}&pageSize={pageSize}&condition={stockCondition}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<PagedStockResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });;
    }
}