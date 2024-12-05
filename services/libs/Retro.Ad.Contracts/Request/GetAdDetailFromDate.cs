namespace Retro.Ad.Contracts.Request;

public record GetAdDetailFromDate(DateTimeOffset FromDate, int PageSize = 10, int PageNumber = 1);