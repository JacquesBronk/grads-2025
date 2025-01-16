namespace Retro.Orders.Contracts.Request;

public record OrderItemRequest(Guid StockItemId, int Quantity);