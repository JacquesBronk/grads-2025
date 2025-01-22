using Microsoft.Extensions.Logging;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Domain;
using Retro.ResultWrappers;

namespace Retro.Ad.Infrastructure;

public class AdService(
    IAdRepository repository, 
    IAdMetricsRepository metricsRepository, 
    IAdsAdminApi adsAdminApi, 
    ILogger<AdService> logger) : IAdService
{
    //Mappers
    private PagedAdDetailResponse PagedAdDetailResponseMapper(PaginatedResult<AdDetail> items)
    {
        return new PagedAdDetailResponse(
            Items: items.Items.Select(AdDetailResponseMapper).AsEnumerable(),
            PageNumber: items.PageNumber,
            PageSize: items.PageSize,
            TotalCount: items.TotalCount
        );
    }

    private PagedAdResponse PagedAdResponseMapper(PaginatedResult<AdDetail> items)
    {
        return new PagedAdResponse(
            Items: items.Items.Select(item => AdMapper(
                item: item,
                callbackUrl: $"http://ads-api:8080/ad-seen/{item.Id}",
                payloadBuilderUpsellUrl: "http://ads-api:8080/{userId}/lu/{unix-epoch}"
            )).AsEnumerable(),
            PageNumber: items.PageNumber,
            PageSize: items.PageSize,
            TotalCount: items.TotalCount
        );
    }

    private AdDetailResponse AdDetailResponseMapper(AdDetail item)
    {
        return new AdDetailResponse(
            Id: item.Id,
            Title: item.Title,
            FullDescription: item.FullDescription,
            ShortDescription: item.ShortDescription,
            ImageUrl: item.ImageUrl,
            StartDateTime: item.StartDateTime,
            EndDateTime: item.EndDateTime,
            IsActive: item.IsActive,
            IsFeatured: item.IsFeatured,
            RenderedHtml: item.RenderedHtml,
            CreatedBy: item.CreatedBy,
            CreatedDateTime: item.CreatedDateTime,
            UpdatedBy: item.UpdatedBy,
            UpdatedDateTime: item.UpdatedDateTime
        );
    }

    private AdDetail AdDetailItemMapper(UpsertAdRequest request)
    {
        return new AdDetail
        {
            Id = request.Id ?? Guid.NewGuid(),
            Title = request.Title,
            FullDescription = request.FullDescription,
            ShortDescription = request.ShortDescription,
            ImageUrl = request.ImageUrl,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            IsActive = request.IsActive,
            IsFeatured = request.IsFeatured,
            RenderedHtml = request.RenderedHtml,
            CreatedBy = request.CreatedBy,
            CreatedDateTime = request.CreatedDateTime,
            UpdatedBy = request.UpdatedBy,
            UpdatedDateTime = request.UpdatedDateTime ?? request.CreatedDateTime
        };
    }

    private Domain.Ad AdMapper(AdDetail item, string callbackUrl, string payloadBuilderUpsellUrl)
    {
        return new Domain.Ad
        {
            Id = item.Id,
            Title = item.Title,
            FullDescription = item.FullDescription,
            ShortDescription = item.ShortDescription,
            ImageUrl = item.ImageUrl,
            StartDateTime = item.StartDateTime,
            EndDateTime = item.EndDateTime,
            IsActive = item.IsActive,
            IsFeatured = item.IsFeatured,
            RenderedHtml = item.RenderedHtml,
            CallbackUrl = callbackUrl,
            PayloadBuilderUpsellUrl = payloadBuilderUpsellUrl
        };
    }

    private AdResponse AdResponseMapper(Domain.Ad item)
    {
        return new AdResponse
        {
            Id = item.Id,
            Title = item.Title,
            FullDescription = item.FullDescription,
            ShortDescription = item.ShortDescription,
            ImageUrl = item.ImageUrl,
            StartDateTime = item.StartDateTime,
            EndDateTime = item.EndDateTime,
            IsActive = item.IsActive,
            IsFeatured = item.IsFeatured,
            RenderedHtml = item.RenderedHtml,
            CallbackUrl = item.CallbackUrl,
            PayloadBuilderUpsellUrl = item.PayloadBuilderUpsellUrl
        };
    }

    public AdResponse MapToAdResponse(AdDetailResponse ad)
    {
        return new AdResponse
        {
            Id = ad.Id,
            CallbackUrl = $"http://ads-api:8080/ad-seen/{ad.Id}",
            PayloadBuilderUpsellUrl = $"http://ads-api:8080/{{userId}}/lu/{{unix-epoch}}"
        };
    }


    public async Task<PagedAdDetailResponse> GetAdDetailFromDate(GetAdDetailFromDate request, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAdDetailFromDate(request.FromDate, request.PageNumber, request.PageSize, cancellationToken);
        return PagedAdDetailResponseMapper(items);
    }

    public async Task<AdDetailResponse> GetAdDetailById(GetAdDetailByIdRequest request, CancellationToken cancellationToken = default)
    {
        var item = await repository.GetByIdAsync(request.Id, cancellationToken);
        return AdDetailResponseMapper(item);
    }

    public async Task<PagedAdDetailResponse> GetAllAds(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
        return PagedAdDetailResponseMapper(items);
    }

    public async Task<PagedAdDetailResponse> GetNAds(GetNAdDetailsRequest request, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetNAd(request.Number, request.PageNumber, request.PageSize, cancellationToken);
        return PagedAdDetailResponseMapper(items);
    }

    public async Task<PagedAdDetailResponse> GetFeatured(GetFeaturedAdsRequest request, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetFeatured(request.FromDate, request.IsActive, request.PageSize, request.PageNumber, cancellationToken);
        return PagedAdDetailResponseMapper(items);
    }

    public async Task<PagedAdResponse> GetFeaturedAds(GetFeaturedAdsRequest item, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetFeatured(item.FromDate, item.IsActive, item.PageSize, item.PageNumber, cancellationToken);
        return PagedAdResponseMapper(items);
    }

    public async Task<IEnumerable<AdDetailResponse>> GetAdsFromTimestampAsync(DateTimeOffset fromDate, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var response = await adsAdminApi.GetAdsFromDateAsync(fromDate, pageNumber, pageSize, cancellationToken);
        if (response.IsSuccessStatusCode) return response.Content?.Items ?? [];
        logger.LogError(response.Error,"Failed to get ads from timestamp, [STATUS CODE: {statusCode}]", response.StatusCode);
        return [];
    }

    public async Task<AdResponse> TrackAdViewAsync(AdViewMetric request, CancellationToken cancellationToken = default)
    {
        var ad = await repository.GetByIdAsync(request.AdId, cancellationToken) ??
                 throw new Exception($"Ad with Id: {request.AdId} not found");
        await metricsRepository.TrackAdViewAsync(request, cancellationToken);
        return MapToAdResponse(AdDetailResponseMapper(ad));
    }

    public async Task AddAdDetail(UpsertAdRequest request, CancellationToken cancellationToken = default)
    {
        var adDetail = AdDetailItemMapper(request);
        await repository.AddAsync(adDetail, cancellationToken);
    }

    public async Task UpdateAdDetail(UpsertAdRequest request, CancellationToken cancellationToken = default)
    {
        var adDetail = AdDetailItemMapper(request);
        await repository.UpdateAsync(adDetail, cancellationToken);
    }

    public async Task DeleteAdDetail(DeleteAdDetailByIdRequest request, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(request.Id, cancellationToken);
    }
}