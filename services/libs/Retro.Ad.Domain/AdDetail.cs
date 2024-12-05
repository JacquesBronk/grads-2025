using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Ad.Domain;

public class AdDetail
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? FullDescription { get; set; }
    public string? ShortDescription { get; set; }
    public string? ImageUrl { get; set; }
    public DateTimeOffset StartDateTime { get; set; }
    public DateTimeOffset EndDateTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public string? RenderedHtml { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset UpdatedDateTime { get; set; }
}