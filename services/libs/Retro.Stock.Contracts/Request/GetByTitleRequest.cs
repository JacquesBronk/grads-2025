namespace Retro.Stock.Contracts.Request;

public record GetByTitleRequest(string Title,int PageNumber = 1, int PageSize = 10);
