using Retro.Orders.Contracts.Response;

namespace Retro.Profile;

public class ProfileService(IProfileRepository profileRepository, Gateway gateway) : IProfileService
{
    public async Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByIdAsync(profileId, cancellationToken);

        if (profile == null)
        {
            return null;
        }
        
        var orders = await gateway.GetOrdersForUserAsync(profile.Id, cancellationToken);

        return new ProfileResponse(profile.Id, profile.UserName, orders);
    }

    public async Task<ProfileResponse?> GetProfileByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByUserNameAsync(userName, cancellationToken);

        if (profile == null)
        {
            return null;
        }
        
        // var orders = await gateway.GetOrdersForUserAsync(profile.Id, cancellationToken);
        var orders = Array.Empty<OrderResponse>();
        
        return new ProfileResponse(profile.Id, profile.UserName, orders);
    }

    public async Task<ProfileResponse?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        var createdProfile = await profileRepository.CreateProfileAsync(profile, cancellationToken);

        if (createdProfile == null)
        {
            return null;
        }
        
        // var orders = await gateway.GetOrdersForUserAsync(createdProfile.Id, cancellationToken);
        var orders = Array.Empty<OrderResponse>();
        return new ProfileResponse(createdProfile.Id, createdProfile.UserName, orders);
    }

    public async Task<ProfileResponse?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        var updatedProfile = await profileRepository.UpdateProfileAsync(profile, cancellationToken);

        if (updatedProfile == null)
        {
            return null;
        }
        
        var orders = await gateway.GetOrdersForUserAsync(updatedProfile.Id, cancellationToken);

        return new ProfileResponse(updatedProfile.Id, updatedProfile.UserName, orders);
    }

    public async Task<ProfileResponse?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken)
    {
        var deletedProfile = await profileRepository.DeleteProfileAsync(profileId, cancellationToken);

        if (deletedProfile == null)
        {
            return null;
        }
        
        var orders = await gateway.GetOrdersForUserAsync(deletedProfile.Id, cancellationToken);

        return new ProfileResponse(deletedProfile.Id, deletedProfile.UserName, orders);
    }
}