namespace Retro.Ad.Contracts.Response;

public record PagedAdResponse(IEnumerable<Domain.Ad> Items, int TotalCount, int PageNumber, int PageSize);