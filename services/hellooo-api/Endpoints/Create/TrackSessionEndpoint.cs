using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Create;

public class TrackSessionEndpoint(ISessionService sessionService) : Endpoint<CreateSessionRequest>
{
    public override void Configure()
    {
        Post("/session/track");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("TrackSession"));
        Summary(s =>
        {
            s.Summary = "Creates a new session record.";
            s.Description = "This endpoint is used to track user sessions.";
            s.Response(200, "Session tracked successfully.");
        });
    }

    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken ct)
    {
        request = request with { IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty };
        request = request with { UserAgent = HttpContext.Request.Headers.UserAgent.ToString() };
        request = request with { UserId = HttpContext.Request.Headers["X-UserId"].ToString() };
        await SendOkAsync(await sessionService.CreateAsync(request, ct), ct);
    }
}