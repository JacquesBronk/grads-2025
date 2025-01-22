namespace Retro.Ad.Contracts.Request;

public record GetFeaturedAdsRequest
{
    public DateTimeOffset FromDate { get; init; } = default;
    public bool IsActive { get; init; } = true;
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;
    
    public GetFeaturedAdsRequest(DateTimeOffset fromDate, bool isActive = true, int pageSize = 10, int pageNumber = 1)
    {
        FromDate = fromDate;
        IsActive = isActive;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
};
