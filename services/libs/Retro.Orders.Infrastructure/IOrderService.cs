using Retro.Orders.Contracts.Request;
using Retro.Orders.Contracts.Response;

namespace Retro.Orders.Infrastructure;

public interface IOrderService
{
    Task<OrderResponse> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task<OrderResponse[]> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken);
    Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order, CancellationToken cancellationToken);
    Task<OrderResponse> CompleteOrderAsync(CompleteOrderRequest order, CancellationToken cancellationToken);
    Task<OrderResponse> CancelOrderAsync(Guid id, CancellationToken cancellationToken);
}