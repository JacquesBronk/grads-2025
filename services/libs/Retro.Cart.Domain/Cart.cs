using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Cart.Domain;

public class Cart
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    public List<CartItem> CartItems { get; set; } = [];
}