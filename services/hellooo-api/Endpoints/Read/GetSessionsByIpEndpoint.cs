using FastEndpoints;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;

namespace Retro.Greeter.Endpoints.Read;

public class GetSessionsByIpEndpoint(ISessionService sessionService) : Endpoint<GetByIpAddressRequest>
{
    public override void Configure()
    {
        Post("/sessions/ip");
        
        AllowAnonymous(); // TODO: Remove this line to require authentication
        
        Description(d =>
        {
            d.WithName("GetSessionByIp");
        });
        Summary(s =>
        {
            s.Summary = "Returns sessions by IP address.";
            s.Description = "This endpoint is used to retrieve sessions by IP address.";
            s.Response(200, "Session returned successfully.");
        });
    }

    public override async Task HandleAsync(GetByIpAddressRequest req, CancellationToken ct) =>
        await SendOkAsync(await sessionService.GetByIpAddressAsync(req, ct), ct);
}