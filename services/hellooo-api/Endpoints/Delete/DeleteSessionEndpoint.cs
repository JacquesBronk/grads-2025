using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Delete;

public class DeleteSessionEndpoint(ISessionService sessionService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete("/session/{Id}");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("DeleteSession"));
        Summary(s =>
        {
            s.Summary = "Deletes a session.";
            s.Description = "This endpoint is used to delete a user session.";
            s.Response(204, "Session deleted successfully.");
        });
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        await sessionService.DeleteAsync(new DeleteSessionRequest(Route<Guid>("id")), ct);
        await SendNoContentAsync(ct);
    }
}