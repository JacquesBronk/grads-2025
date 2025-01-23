using System.Security.Claims;
using FastEndpoints;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;

namespace Retro.Greeter.Endpoints;

public class GreetingEndpoint(IAdService adService) : EndpointWithoutRequest<GreetResponse>
{
    public override void Configure()
    {
        Get("/greeter");
        AllowAnonymous();
        
        Description(d => d.WithName("Greeting"));
        Summary(s =>
        {
            s.Summary = "Returns a greeting message to the user.";
            s.Description = "If the user is authenticated, the greeting will be personalized using their name. Otherwise, a generic greeting and sign-up prompt will be returned.";
            s.Response<GreetResponse>(200, "Greeting returned successfully.");
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userName = HttpContext.Request.Headers["X-UserName"].ToString();
        var isAuthenticated = string.IsNullOrWhiteSpace(userName);
        
        var ads = isAuthenticated
            ? await GetPersonalizedAdsAsync(ct)
            : await GetFeaturedAdsAsync(ct);
        
        var response = new GreetResponse
        {
            Message = isAuthenticated
                ? $"Hi, {userName}! Welcome back to Retro Shop."
                : "Hello, Guest. Consider signing up!",
            SignupSpecial = isAuthenticated ? null : "Sign up now for a special discount!",
            Ads = ads
        };

        await SendOkAsync(response, ct);
    }
    
    private async Task<List<AdResponse>> GetPersonalizedAdsAsync(CancellationToken ct)
    {
        var userId = HttpContext.Request.Headers["X-UserId"].ToString();
        var unixEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        return await adService.GetPersonalizedAdsAsync(userId, unixEpoch, ct);
    }
    
    private async Task<List<AdResponse>> GetFeaturedAdsAsync(CancellationToken ct)
    {
        var request = new GetFeaturedAdsRequest(fromDate: DateTimeOffset.UtcNow);
        return await adService.GetFeaturedAdsAsync(request, ct);
    }
}  