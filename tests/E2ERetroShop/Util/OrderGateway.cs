using System.Text.Json;
using Retro.Http;
using Retro.Orders.Contracts.Request;
using Retro.Orders.Contracts.Response;

namespace E2ERetroShop.Util;

public class OrderGateway
{
    private readonly string _baseUrl = "http://localhost:5000/orders-api/order";
    
    public async Task<OrderResponse> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/{orderId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<OrderResponse[]> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/GetByUserId/{userId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse[]>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/create";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<OrderResponse> CompleteOrderAsync(CompleteOrderRequest order, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/complete";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Patch)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<OrderResponse> CancelOrderAsync(Guid id, CancellationToken cancellationToken)
    {
        var stockItemUrl = $"{_baseUrl}/cancel/{id}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Patch)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
}
