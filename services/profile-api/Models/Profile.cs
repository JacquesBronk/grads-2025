using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Retro.Profile.Models;

public class Profile
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}