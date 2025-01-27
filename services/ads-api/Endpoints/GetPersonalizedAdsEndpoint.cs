using FastEndpoints;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Infrastructure;

namespace Retro.Ads.Endpoints;

public class GetPersonalizedAdsEndpoint(IAdService adService, IAdMetricsRepository adMetricsRepository) : EndpointWithoutRequest<IEnumerable<AdResponse>>
{
    public override void Configure()
    {
        Get("ads/{userId}/lu/{unixEpoch}");
        AllowAnonymous();
        
        Description(d => d.WithName("PersonalizedAds"));
        Summary(s =>
        {
            s.Summary = "Get personalized ads.";
            s.Description = "This endpoint is used to get personalized ads.";
            s.Response<IEnumerable<AdResponse>>(200, "Personalized ads retrieved successfully.");
        });
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<string>("userId") ?? "";
        var unixEpoch = Route<long>("unixEpoch");
        
        var timestamp = DateTimeOffset.FromUnixTimeSeconds(unixEpoch);
        
        var viewedAdIds = await adMetricsRepository.GetViewedAdIdsAsync(userId, ct);
        
        var ads = await adService.GetAdsFromTimestampAsync(timestamp, 1, 15, ct);
        var unseenAds = ads.Where(ad => !viewedAdIds.Contains(ad.Id));
        
        var personalizedAds = unseenAds.Select(adService.MapToAdResponse);
        await SendOkAsync(personalizedAds, ct);
    }
}