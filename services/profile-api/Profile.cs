using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Profile;

public class Profile
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public string UserName { get; set; }
}