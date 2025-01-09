using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;

namespace Retro.Greeter.Infrastructure;

public interface ISessionService
{
    Task<PagedSessionResponse> GetAllAsync(GetAllByPageRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetActiveSessionsAsync(GetAllByPageRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByIdAsync(GetByIdRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByUserIdAsync(GetByUserIdRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByRouteAsync(GetByRouteRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByUserAgentAsync(GetByUserAgentRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByIpAddressAsync(GetByIpAddressRequest request, CancellationToken cancellationToken);
    Task CreateAsync(CreateSessionRequest request, CancellationToken cancellationToken);
    Task UpdateStateAsync(UpdateSessionRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteSessionRequest request, CancellationToken cancellationToken);
}