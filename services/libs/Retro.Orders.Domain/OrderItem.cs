using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Orders.Domain;

public class OrderItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid StockItemId { get; set; }
    public int Quantity { get; set; }
    public decimal StockItemPrice { get; set; }
    public decimal OrderItemPrice { get; set; }
    public bool IsDiscounted { get; set; }
    public double DiscountPercentage { get; set; }
    
    public OrderItem(Guid stockItemId, int quantity, decimal stockItemPrice)
    {
        StockItemId = stockItemId;
        Quantity = quantity;
        StockItemPrice = stockItemPrice;
        OrderItemPrice = Quantity * StockItemPrice;
    }
}