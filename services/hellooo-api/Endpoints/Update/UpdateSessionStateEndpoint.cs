using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Update;

public class UpdateSessionStateEndpoint(ISessionService sessionService) : Endpoint<UpdateSessionRequest>
{
    public override void Configure()
    {
        Put("/session/{Id}");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("UpdateSession"));
        Summary(s =>
        {
            s.Summary = "Updates a session.";
            s.Description = "This endpoint is used to update a user session.";
            s.Response(200, "Session updated successfully.");
        });
    }
    
    public override async Task HandleAsync(UpdateSessionRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.UpdateStateAsync(request, ct), ct);
}