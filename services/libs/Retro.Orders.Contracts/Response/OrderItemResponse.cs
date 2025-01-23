namespace Retro.Orders.Contracts.Response;

public record OrderItemResponse(
    Guid StockItemId, 
    int Quantity, 
    decimal StockItemPrice, 
    decimal OrderItemPrice, 
    string Name, 
    string Description, 
    bool IsDiscounted, 
    double DiscountPercentage, 
    string ImageUrl);