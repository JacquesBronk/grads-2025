using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Domain;

/// <summary>
/// MongoDB document representing a session.
/// </summary>
public class Session
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public long EntryEpoch { get; set; }
    public long ExitEpoch { get; set; }
    public string Route { get; set; }
    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
    public bool IsActive { get; set; }
}
