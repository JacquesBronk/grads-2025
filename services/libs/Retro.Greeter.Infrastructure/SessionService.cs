using Retro.Domain;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;
using Retro.ResultWrappers;

namespace Retro.Greeter.Infrastructure;

public class SessionService(ISessionRepository repository) : ISessionService
{
    
    #region Mappers
    
    private PagedSessionResponse PagedSessionResponseMapper(PaginatedResult<Session> items) => 
        new (
            Items: items.Items.Select(SessionResponseMapper).AsEnumerable(),
            TotalCount: items.TotalCount,
            PageNumber: items.PageNumber,
            PageSize: items.PageSize
        );

    private SessionResponse SessionResponseMapper(Session session) =>
        new (
            Id: session.Id,
            UserId: session.UserId,
            Route: session.Route,
            UserAgent: session.UserAgent,
            IpAddress: session.IpAddress,
            EntryEpoch: session.EntryEpoch,
            ExitEpoch: session.ExitEpoch,
            IsActive: session.IsActive
        );

    private Session CreateSessionMapper(CreateSessionRequest request) =>
        new()
        {
            UserId = request.UserId,
            Route = request.Route,
            UserAgent = request.UserAgent,
            IpAddress = request.IpAddress,
            EntryEpoch = request.EntryEpoch,
            ExitEpoch = request.ExitEpoch,
            IsActive = true // default to true when creating a session
        };

    #endregion
    
    public async Task<PagedSessionResponse> GetAllAsync(GetAllByPageRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken));

    public async Task<PagedSessionResponse> GetActiveSessionsAsync(GetAllByPageRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetActiveSessionsAsync(request.PageNumber, request.PageSize, cancellationToken));

    public async Task<SessionResponse> GetByIdAsync(GetByIdRequest request, CancellationToken cancellationToken) => 
        SessionResponseMapper(await repository.GetByIdAsync(request.Id, cancellationToken));

    public async Task<PagedSessionResponse> GetByUserIdAsync(GetByUserIdRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetByUserIdAsync(request.UserId, request.PageNumber, request.PageSize, cancellationToken));

    public async Task<PagedSessionResponse> GetByRouteAsync(GetByRouteRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetByRouteAsync(request.Route, request.PageNumber, request.PageSize, cancellationToken));

    public async Task<PagedSessionResponse> GetByUserAgentAsync(GetByUserAgentRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetByUserAgentAsync(request.UserAgent, request.PageNumber, request.PageSize, cancellationToken));

    public async Task<PagedSessionResponse> GetByIpAddressAsync(GetByIpAddressRequest request, CancellationToken cancellationToken) => 
        PagedSessionResponseMapper(await repository.GetByIpAddressAsync(request.IpAddress, request.PageNumber, request.PageSize, cancellationToken));

    // TODO: Possibly retrieve some information from the http context?
    public async Task<SessionResponse> CreateAsync(CreateSessionRequest request, CancellationToken cancellationToken) => 
        SessionResponseMapper(await repository.CreateAsync(CreateSessionMapper(request), cancellationToken));

    public async Task<SessionResponse> UpdateStateAsync(UpdateSessionRequest request, CancellationToken cancellationToken)
    {
        var session = await repository.GetByIdAsync(request.Id, cancellationToken) ?? 
                      throw new Exception("Session not found");
        
        session.IsActive = !session.IsActive;
        return SessionResponseMapper(await repository.UpdateAsync(session, cancellationToken));
    }

    public async Task DeleteAsync(DeleteSessionRequest request, CancellationToken cancellationToken) => 
        await repository.DeleteAsync(request.Id, cancellationToken);
}