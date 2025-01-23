using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Ad.Domain;

public class AdViewMetric
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid AdId { get; set; }
    public string? UserId { get; set; }
    public DateTimeOffset ViewedAt { get; set; }
    public string? UserAgent { get; set; }
    public string? IpAddress { get; set; }
}