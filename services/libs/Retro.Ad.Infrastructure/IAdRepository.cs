using Retro.Ad.Domain;
using Retro.ResultWrappers;

namespace Retro.Ad.Infrastructure;

public interface IAdRepository
{
    Task<AdDetail> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedResult<AdDetail>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(AdDetail adDetail, CancellationToken cancellationToken = default);
    Task UpdateAsync(AdDetail adDetail, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}