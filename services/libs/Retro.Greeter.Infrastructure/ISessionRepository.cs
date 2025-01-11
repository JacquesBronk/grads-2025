using Retro.Domain;
using Retro.ResultWrappers;

namespace Retro.Greeter.Infrastructure;

public interface ISessionRepository
{
    Task<PaginatedResult<Session>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetActiveSessionsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Session> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Session> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<Session> GetByRouteAsync(string route, CancellationToken cancellationToken = default);
    Task<Session> GetByUserAgentAsync(string userAgent, CancellationToken cancellationToken = default);
    Task<Session> GetByIpAddressAsync(string ipAddress, CancellationToken cancellationToken = default);
    Task<Session> CreateAsync(Session session, CancellationToken cancellationToken = default);
    Task<Session> UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}