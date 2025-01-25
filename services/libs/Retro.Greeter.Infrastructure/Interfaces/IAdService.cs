using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;

namespace Retro.Greeter.Infrastructure;

public interface IAdService
{
    Task<List<AdResponse>> GetPersonalizedAdsAsync(string userId, long unixEpoch, CancellationToken cancellationToken);
    Task<List<AdResponse>> GetFeaturedAdsAsync(GetFeaturedAdsRequest request, CancellationToken cancellationToken);
}