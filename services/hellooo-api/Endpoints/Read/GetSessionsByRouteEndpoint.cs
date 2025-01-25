using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Read;

public class GetSessionsByRouteEndpoint(ISessionService sessionService) : Endpoint<GetByRouteRequest>
{
    public override void Configure()
    {
        Post("/sessions/route");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("GetSessionsByRoute"));
        Summary(s =>
        {
            s.Summary = "Returns a list of sessions by route.";
            s.Description = "This endpoint is used to retrieve a list of all user sessions by route.";
            s.Response<PagedSessionResponse>(200, "Sessions returned successfully.");
        });
    }
    
    public override async Task HandleAsync(GetByRouteRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByRouteAsync(request, ct), ct);
}