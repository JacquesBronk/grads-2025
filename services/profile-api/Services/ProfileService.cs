using Retro.Orders.Contracts.Response;
using Retro.Profile.Interfaces;
using Retro.Profile.Response;

namespace Retro.Profile.Services;

public class ProfileService(IProfileRepository profileRepository, Gateway.Gateway gateway) : IProfileService
{
    public async Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByIdAsync(profileId, cancellationToken);

        if (profile == null) 
            return null;

        var orders = Array.Empty<OrderResponse>();
        try
        {
            orders = await gateway.GetOrdersForUserAsync(Guid.Parse(profile.UserId), cancellationToken);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new ProfileResponse(profile.Id, profile.UserId, profile.UserName, profile.Email, orders);
    }

    public async Task<ProfileResponse?> GetProfileByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileByUserIdAsync(userId, cancellationToken);

        if (profile == null) 
            return null;

        var orders = Array.Empty<OrderResponse>();
        try
        {
            orders = await gateway.GetOrdersForUserAsync(Guid.Parse(profile.UserId), cancellationToken);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        return new ProfileResponse(profile.Id, profile.UserId, profile.UserName, profile.Email, orders);
    }

    public async Task<ProfileResponse?> CreateProfileAsync(Models.Profile profile, CancellationToken cancellationToken)
    {
        var existingProfile = await GetProfileByUserIdAsync(profile.UserId, cancellationToken);
        if (existingProfile != null)
            return existingProfile;
        
        var createdProfile = await profileRepository.CreateProfileAsync(profile, cancellationToken);

        if (createdProfile == null) 
            return null;

        var orders = Array.Empty<OrderResponse>();
        try
        {
            orders = await gateway.GetOrdersForUserAsync(Guid.Parse(createdProfile.UserId), cancellationToken);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        return new ProfileResponse(profile.Id, profile.UserId, profile.UserName, profile.Email, orders);
    }

    public async Task<ProfileResponse?> UpdateProfileAsync(Models.Profile profile, CancellationToken cancellationToken)
    {
        var existingProfile = await GetProfileByUserIdAsync(profile.UserId, cancellationToken);
        profile.Id = existingProfile?.Id ?? Guid.Empty;
        
        var updatedProfile = await profileRepository.UpdateProfileAsync(profile, cancellationToken);

        if (updatedProfile == null) 
            return null;

        var orders = Array.Empty<OrderResponse>();
        try
        {
            orders = await gateway.GetOrdersForUserAsync(Guid.Parse(profile.UserId), cancellationToken);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new ProfileResponse(updatedProfile.Id, updatedProfile.UserId, updatedProfile.UserName, updatedProfile.Email, orders);
    }

    public async Task<ProfileResponse?> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken)
    {
        var deletedProfile = await profileRepository.DeleteProfileAsync(profileId, cancellationToken);

        if (deletedProfile == null) 
            return null;

        var orders = Array.Empty<OrderResponse>();
        try
        {
            orders = await gateway.GetOrdersForUserAsync(Guid.Parse(deletedProfile.UserId), cancellationToken);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new ProfileResponse(deletedProfile.Id, deletedProfile.UserId, deletedProfile.UserName, deletedProfile.Email, orders);
    }
}