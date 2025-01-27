using MongoDB.Driver;
using Retro.Persistence.Mongo.Infra;
using Retro.Profile.Interfaces;

namespace Retro.Profile.Repositories;

public class ProfileRepository(IMongoDbContext mongoDbContext) : IProfileRepository
{
    private readonly IMongoCollection<Models.Profile> _collection = mongoDbContext.GetCollection<Models.Profile>("profiles");
    
    public async Task<Models.Profile?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken)
    {
        return await _collection.Find(p => p.Id == profileId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Models.Profile?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await _collection.Find(p => p.UserId == userId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Models.Profile?> CreateProfileAsync(Models.Profile profile, CancellationToken cancellationToken)
    {
        if (profile.Id == Guid.Empty)
        {
            profile.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(profile, new InsertOneOptions(), cancellationToken: cancellationToken);

        return profile;
    }

    public async Task<Models.Profile?> UpdateProfileAsync(Models.Profile profile, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(p => p.Id == profile.Id, profile, cancellationToken: cancellationToken);

        return profile;
    }

    public async Task<Models.Profile?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken)
    {
        return await _collection.FindOneAndDeleteAsync(p => p.Id == profileId, cancellationToken: cancellationToken);
    }
}