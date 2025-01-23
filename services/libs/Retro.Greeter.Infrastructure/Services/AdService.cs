using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;

namespace Retro.Greeter.Infrastructure.Services;

public class AdService(IAdsApi adsApi) : IAdService
{
    // Mappers
    private List<AdResponse> AdResponseMapper(PagedAdResponse ads) => 
        ads.Items.Select(ad => new AdResponse(ad)).ToList();
    public async Task<List<AdResponse>> GetPersonalizedAdsAsync(string userId, long unixEpoch, CancellationToken cancellationToken)
    {
        var response = await adsApi.GetPersonalizedAsync(userId, unixEpoch, cancellationToken);
        if (response.IsSuccessStatusCode) return response.Content?.ToList() ?? [];
        return [];
    }

    public async Task<List<AdResponse>> GetFeaturedAdsAsync(GetFeaturedAdsRequest request, CancellationToken cancellationToken)
    {
        var response = await adsApi.GetFeaturedAdsAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode) return [];
        return response.Content != null ? AdResponseMapper(response.Content) : [];
    }
}