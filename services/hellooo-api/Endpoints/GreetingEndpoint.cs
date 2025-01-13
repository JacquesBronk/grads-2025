using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Swagger;
using Retro.Greeter.Contracts.Response;

namespace Retro.Greeter.Endpoints;

public class GreetingEndpoint : EndpointWithoutRequest<GreetResponse>
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
        var userLoggedIn = User.Identity?.IsAuthenticated ?? false;
        var username = userLoggedIn ? User.FindFirstValue(ClaimTypes.Name) : "Guest";

        await SendOkAsync(new GreetResponse
        {
            Message = userLoggedIn
                ? $"Hi, {username}! Welcome back to Retro Shop."
                : "Hello, Guest. Consider signing up!",
            SignupSpecial = userLoggedIn ? null : "Sign up now for a special discount!",
            Ads = []
        }, ct);
    }
}  