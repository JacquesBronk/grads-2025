using FastEndpoints;
using Retro.Domain;

namespace Retro.Greeter.Contracts.Request;

public record UpdateSessionRequest
{
    [RouteParam, BindFrom("id")] 
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public long EntryEpoch   { get; init; }
    public long ExitEpoch{ get; init; }
    public string Route  { get; init; }
    public string UserAgent  { get; init; }
    public string IpAddress  { get; init; }
    public bool IsActive  { get; init; }
    
    public UpdateSessionRequest(Session session)
    {
        Id = session.Id;
        UserId = session.UserId;
        EntryEpoch = session.EntryEpoch;
        ExitEpoch = session.ExitEpoch;
        Route = session.Route;
        UserAgent = session.UserAgent;
        IpAddress = session.IpAddress;
        IsActive = session.IsActive;
    }
}

