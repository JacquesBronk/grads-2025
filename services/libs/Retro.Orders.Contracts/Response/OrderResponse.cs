namespace Retro.Orders.Contracts.Response;

public record OrderResponse(Guid Id, Guid UserId, Guid? PaymentId, OrderStatus OrderStatus, decimal OrderTotal, OrderItemResponse[] OrderItems);