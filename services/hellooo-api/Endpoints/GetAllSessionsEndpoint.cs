using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;

namespace Retro.Greeter.Endpoints;

public class GetAllSessionsEndpoint(ISessionService sessionService) : Endpoint<GetAllByPageRequest>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("GetAllSessions"));
        Summary(s =>
        {
            s.Summary = "Returns a list of all sessions.";
            s.Description = "This endpoint is used to retrieve a list of all user sessions.";
            s.Response<PagedSessionResponse>(200, "Sessions returned successfully.");
        });
    }
    
    public override async Task HandleAsync(GetAllByPageRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetAllAsync(request, ct), ct);
}