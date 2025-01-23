using Refit;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Domain;

namespace Retro.Ad.Infrastructure;

public interface IAdsAdminApi
{
    [Get("/ads/from-date")]
    Task<ApiResponse<PagedAdDetailResponse>> GetAdsFromDateAsync(
        [Query] DateTimeOffset fromDate,
        [Query] int pageNumber,
        [Query] int pageSize,
        CancellationToken cancellationToken = default);
}