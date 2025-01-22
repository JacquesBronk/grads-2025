namespace Retro.Orders.Contracts.Request;

public record CreateOrderRequest(Guid UserId, OrderItemRequest[] OrderItems);