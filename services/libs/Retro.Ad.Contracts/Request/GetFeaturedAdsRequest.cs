namespace Retro.Ad.Contracts.Request;

public record GetFeaturedAdsRequest(DateTimeOffset FromDate = default, bool IsActive = true, int PageSize = 10, int PageNumber = 1);
