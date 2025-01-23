using Refit;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;

namespace Retro.Greeter.Infrastructure;

public interface IAdsApi
{
    [Post("/ads/featured")]
    Task<ApiResponse<PagedAdResponse>> GetFeaturedAdsAsync(GetFeaturedAdsRequest request, CancellationToken cancellationToken = default);
    
    [Get("/ads/{userId}/lu/{unixEpoch}")]
    Task<ApiResponse<IEnumerable<AdResponse>>> GetPersonalizedAsync(string userId, long unixEpoch, CancellationToken cancellationToken = default);
}