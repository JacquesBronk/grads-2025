using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Contracts.Response;

namespace Retro.Greeter.Infrastructure.Interfaces;

public interface ISessionService
{
    Task<PagedSessionResponse> GetAllAsync(GetAllByPageRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetActiveSessionsAsync(GetAllByPageRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> GetByIdAsync(GetByIdRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetByUserIdAsync(GetByUserIdRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetByRouteAsync(GetByRouteRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetByUserAgentAsync(GetByUserAgentRequest request, CancellationToken cancellationToken);
    Task<PagedSessionResponse> GetByIpAddressAsync(GetByIpAddressRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> CreateAsync(CreateSessionRequest request, CancellationToken cancellationToken);
    Task<SessionResponse> UpdateStateAsync(UpdateSessionRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteSessionRequest request, CancellationToken cancellationToken);
}