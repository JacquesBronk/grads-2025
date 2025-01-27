using Retro.Profile.Response;

namespace Retro.Profile.Interfaces;

public interface IProfileService
{
    Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken);
    Task<ProfileResponse?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<ProfileResponse?> CreateProfileAsync(Models.Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> UpdateProfileAsync(Models.Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
}