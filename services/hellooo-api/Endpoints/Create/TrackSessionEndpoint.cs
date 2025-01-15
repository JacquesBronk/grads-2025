using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;

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
    
    public override async Task HandleAsync(CreateSessionRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.CreateAsync(request, ct), ct);
}