using System.Text.Json;
using Retro.Http;
using Retro.Orders.Contracts.Response;

namespace Retro.Profile;

public class Gateway(string ordersServiceAddress)
{
    public async Task<OrderResponse[]> GetOrdersForUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var OrdersUrl = $"{ordersServiceAddress}/orders-api/GetByUserId/{userId}";
        var OrdersResponse = await new RequestBuilder()
            .For(new Uri(OrdersUrl))
            .WithMethod(HttpMethod.Get)
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<OrderResponse[]>(OrdersResponse, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true }) ?? [];
    }
}