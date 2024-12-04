namespace Retro.Stock.Contracts.Response;

public record StockResponse(Guid Id, string Sku, string Name, string Description, decimal Price, int Quantity, string[] Tags, bool IsDiscounted, double DiscountPercentage, string ImageUrl);
