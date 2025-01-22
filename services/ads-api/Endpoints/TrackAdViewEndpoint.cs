using FastEndpoints;
using Retro.Ad.Domain;
using Retro.Ad.Infrastructure;

namespace Retro.Ads.Endpoints;

public class TrackAdViewEndpoint(IAdService service) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/ad-seen/{id}/user/{userId}");
        AllowAnonymous();
        
        Description(d => d.WithName("TrackAdView"));
        Summary(s =>
        {
            s.Summary = "Track ad view.";
            s.Description = "This endpoint is used to track ad views.";
            s.Response(200, "Ad view tracked successfully.");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var adId = Route<Guid>("id");
        var userId = Route<string>("userId");
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        
        var metric = new AdViewMetric
        {
            AdId = adId,
            UserId = userId,
            UserAgent = userAgent,
            IpAddress = ipAddress,
            ViewedAt = DateTimeOffset.UtcNow
        };

        await service.TrackAdViewAsync(metric, ct);
        await SendOkAsync(new
        {
            Message = "Ad view tracked successfully.",
            AdId = adId,
            Success = true
        }, ct);
    }
}