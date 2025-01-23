using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Interfaces;

namespace Retro.Greeter.Endpoints.Read;

public class GetSessionByIdEndpoint(ISessionService sessionService) : EndpointWithoutRequest<SessionResponse>
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

    public override async Task HandleAsync(CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByIdAsync(
            new GetByIdRequest(Route<Guid>("id")), ct), ct);
}