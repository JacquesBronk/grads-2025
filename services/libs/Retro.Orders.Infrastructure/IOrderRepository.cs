using Retro.Orders.Contracts;
using Retro.Orders.Domain;

namespace Retro.Orders.Infrastructure;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task<Order[]> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken);
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
    Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
}