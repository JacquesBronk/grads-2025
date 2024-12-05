using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Domain;
using Retro.ResultWrappers;

namespace Retro.Ad.Infrastructure;

public class AdService(IAdRepository repository): IAdService
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