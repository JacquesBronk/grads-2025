using Retro.Ad.Domain;
using Retro.ResultWrappers;

namespace Retro.Ad.Infrastructure;

public interface IAdRepository
{
    Task<AdDetail> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedResult<AdDetail>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<AdDetail>> GetAdDetailFromDate(DateTimeOffset dateTimeOffset, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<AdDetail>> GetNAd(int amount,int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PaginatedResult<AdDetail>> GetFeatured(DateTimeOffset fromDate = default, bool isActive = true, int pageSize = 10, int pageNumber = 1, CancellationToken cancellationToken = default);
    Task AddAsync(AdDetail adDetail, CancellationToken cancellationToken = default);
    Task UpdateAsync(AdDetail adDetail, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}