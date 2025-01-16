namespace Retro.Orders.Contracts.Request;

public record CompleteOrderRequest(Guid OrderId, Guid PaymentId);