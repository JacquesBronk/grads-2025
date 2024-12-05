using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;

namespace Retro.Ad.Infrastructure;

public interface IAdService
{
    Task<PagedAdDetailResponse> GetAdDetailFromDate(GetAdDetailFromDate request, CancellationToken cancellationToken = default);
    Task<AdDetailResponse> GetAdDetailById(GetAdDetailByIdRequest request, CancellationToken cancellationToken = default);
    Task<PagedAdDetailResponse> GetAllAds(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedAdDetailResponse> GetNAds(GetNAdDetailsRequest request, CancellationToken cancellationToken = default);
    Task<PagedAdDetailResponse> GetFeatured(GetFeaturedAdsRequest request, CancellationToken cancellationToken = default);
    Task AddAdDetail(UpsertAdRequest request, CancellationToken cancellationToken = default);
    Task UpdateAdDetail(UpsertAdRequest request, CancellationToken cancellationToken = default);
    Task DeleteAdDetail(DeleteAdDetailByIdRequest request, CancellationToken cancellationToken = default);
}