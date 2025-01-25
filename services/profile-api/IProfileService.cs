namespace Retro.Profile;

public interface IProfileService
{
    Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken);
    Task<ProfileResponse?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<ProfileResponse?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
}