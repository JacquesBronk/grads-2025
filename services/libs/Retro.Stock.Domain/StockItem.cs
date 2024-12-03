using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Stock.Domain;

/// <summary>
/// MongoDB document representing a stock item.
/// </summary>
public class StockItem
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public string Sku { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public StockCondition Condition { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsDiscounted { get; set; }
    public double DiscountPercentage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public string CreatedBy { get; set; }
    public string[] Tags { get; set; }
}