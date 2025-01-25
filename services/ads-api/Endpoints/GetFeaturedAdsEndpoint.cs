using FastEndpoints;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Infrastructure;

namespace Retro.Ads.Endpoints;

public class GetFeaturedAdsEndpoint(IAdService adService) : Endpoint<GetFeaturedAdsRequest>
{
    public override void Configure()
    {
        Post("/ads/featured");
        AllowAnonymous();
        
        Description(d => d.WithName("GetFeaturedAds"));
        Summary(s =>
        {
            s.Summary = "Get featured ads.";
            s.Description = "This endpoint is used to get featured ads.";
            s.Response<PagedAdResponse>(200, "Featured ads retrieved successfully.");
        });
    }
    
    public override async Task HandleAsync(GetFeaturedAdsRequest request, CancellationToken ct) =>
        await SendOkAsync(await adService.GetFeaturedAds(request, ct), ct);
}