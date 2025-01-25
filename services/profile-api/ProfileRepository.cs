using MongoDB.Driver;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Profile;

public class ProfileRepository(IMongoDbContext mongoDbContext) : IProfileRepository
{
    private readonly IMongoCollection<Profile> _collection = mongoDbContext.GetCollection<Profile>("profiles");
    
    public async Task<Profile?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken)
    {
        return await _collection.Find(p => p.Id == profileId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Profile?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _collection.Find(p => p.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Profile?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        if (profile.Id == Guid.Empty)
        {
            profile.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(profile, new InsertOneOptions(), cancellationToken: cancellationToken);

        return profile;
    }

    public async Task<Profile?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(p => p.Id == profile.Id, profile, cancellationToken: cancellationToken);

        return profile;
    }

    public async Task<Profile?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken)
    {
        return await _collection.FindOneAndDeleteAsync(p => p.Id == profileId, cancellationToken: cancellationToken);
    }
}