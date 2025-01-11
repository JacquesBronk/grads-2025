using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;

namespace Retro.Greeter.Endpoints;

public class GetSessionByIdEndpoint(ISessionService sessionService) : Endpoint<GetByIdRequest>
{
    public override void Configure()
    {
        Get("/session/{id}");
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d => d.WithName("GetSessionById"));
        Summary(s =>
        {
            s.Summary = "Returns a session by ID.";
            s.Description = "This endpoint is used to retrieve a session by its ID.";
            s.Response(200, "Session returned successfully.");
        });
    }
    
    public override async Task HandleAsync(GetByIdRequest request, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByIdAsync(request, ct), ct);
}