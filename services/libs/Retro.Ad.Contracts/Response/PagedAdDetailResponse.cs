namespace Retro.Ad.Contracts.Response;

public record PagedAdDetailResponse(IEnumerable<AdDetailResponse> Items, int TotalCount, int PageNumber, int PageSize);