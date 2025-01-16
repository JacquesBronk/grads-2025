using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Retro.Orders.Contracts;

namespace Retro.Orders.Domain;

public class Order
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid? PaymentId { get; set; }
    public decimal OrderTotal { get; set; }
    public OrderItem[] OrderItems { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.NotPaid;
    
    public Order(Guid userId, Guid? paymentId, OrderItem[] orderItems)
    {
        UserId = userId;
        PaymentId = paymentId;
        OrderItems = orderItems;
        OrderTotal = orderItems.Sum(x => x.OrderItemPrice);
    }
}