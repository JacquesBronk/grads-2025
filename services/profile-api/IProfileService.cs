namespace Retro.Profile;

public interface IProfileService
{
    Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken);
    Task<ProfileResponse?> GetProfileByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<ProfileResponse?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<ProfileResponse?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
}