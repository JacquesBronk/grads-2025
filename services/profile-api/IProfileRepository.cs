namespace Retro.Profile;

public interface IProfileRepository
{
    Task<Profile?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken);
    Task<Profile?> GetProfileByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<Profile?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<Profile?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken);
    Task<Profile?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
}