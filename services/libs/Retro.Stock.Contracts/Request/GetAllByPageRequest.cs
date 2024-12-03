namespace Retro.Stock.Contracts.Request;

public record GetAllByPageRequest(int PageNumber = 1, int PageSize = 10);