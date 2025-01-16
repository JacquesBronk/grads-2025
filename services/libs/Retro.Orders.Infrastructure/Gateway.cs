using System.Text.Json;
using Retro.Http;
using Retro.Stock.Contracts.Response;

namespace Retro.Orders.Infrastructure;

public class Gateway
{
    private readonly string _stockServiceAddress;

    public Gateway(string stockServiceAddress)
    {
        _stockServiceAddress = stockServiceAddress;
    }
    
    public async Task<StockResponse> GetStockItemAsync(Guid stockItemId, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_stockServiceAddress}/stock/{stockItemId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<StockResponse>(stockItemResponse, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
}
