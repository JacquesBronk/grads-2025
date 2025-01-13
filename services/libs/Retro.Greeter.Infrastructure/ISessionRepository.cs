using Retro.Domain;
using Retro.ResultWrappers;

namespace Retro.Greeter.Infrastructure;

public interface ISessionRepository
{
    Task<PaginatedResult<Session>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetActiveSessionsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetByUserIdAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetByRouteAsync(string route, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetByUserAgentAsync(string userAgent, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<Session>> GetByIpAddressAsync(string ipAddress, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Session> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Session> CreateAsync(Session session, CancellationToken cancellationToken = default);
    Task<Session> UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}