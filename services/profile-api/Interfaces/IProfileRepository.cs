namespace Retro.Profile.Interfaces;

public interface IProfileRepository
{
    Task<Models.Profile?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken);
    Task<Models.Profile?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<Models.Profile?> CreateProfileAsync(Models.Profile profile, CancellationToken cancellationToken);
    Task<Models.Profile?> UpdateProfileAsync(Models.Profile profile, CancellationToken cancellationToken);
    Task<Models.Profile?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
}