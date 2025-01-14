using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;

namespace Retro.Greeter.Endpoints.Read;

public class GetSessionsByUserIdEndpoint(ISessionService sessionService) : Endpoint<GetByUserIdRequest>
{
    public override void Configure()
    {
        Post("/sessions/user");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("GetSessionsByUserId"));
        Summary(s =>
        {
            s.Summary = "Returns a list of sessions by user ID.";
            s.Description = "This endpoint is used to retrieve a list of all user sessions by user ID.";
            s.Response<PagedSessionResponse>(200, "Sessions returned successfully.");
        });
    }
    
    public override async Task HandleAsync(GetByUserIdRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByUserIdAsync(request, ct), ct);
}