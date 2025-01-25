using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Read;

public class GetSessionsByUserAgentEndpoint(ISessionService sessionService) : Endpoint<GetByUserAgentRequest>
{
    public override void Configure()
    {
        Post("/sessions/user-agent");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("GetSessionsByUserAgent"));
        Summary(s =>
        {
            s.Summary = "Returns a list of sessions by user agent.";
            s.Description = "This endpoint is used to retrieve a list of all user sessions by user agent.";
            s.Response<PagedSessionResponse>(200, "Sessions returned successfully.");
        });
    }
    
    public override async Task HandleAsync(GetByUserAgentRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByUserAgentAsync(request, ct), ct);
}