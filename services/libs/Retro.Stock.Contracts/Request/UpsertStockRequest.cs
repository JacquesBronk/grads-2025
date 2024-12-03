using Retro.Stock.Domain;

namespace Retro.Stock.Contracts.Request;

/// <summary>
/// Request object for creating or updating a stock item
/// </summary>
/// <param name="Id">Leave the Id Blank to create a new item</param>
/// <param name="Sku"></param>
/// <param name="Title"></param>
/// <param name="Description"></param>
/// <param name="ImageUrl"></param>
/// <param name="Condition"></param>
/// <param name="Price"></param>
/// <param name="Quantity"></param>
/// <param name="Tags"></param>
/// <param name="IsDiscounted"></param>
/// <param name="DiscountPercentage"></param>
/// <param name="CreatedAt"></param>
/// <param name="UpdatedAt"></param>
/// <param name="CreatedBy"></param>
/// <param name="UpdatedBy"></param>
public record UpsertStockRequest(Guid? Id, string Sku, string Title, string Description, string ImageUrl, 
    StockCondition Condition, decimal Price, int Quantity, string[] Tags, bool IsDiscounted, double DiscountPercentage, 
    DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt, string CreatedBy, string UpdatedBy);
