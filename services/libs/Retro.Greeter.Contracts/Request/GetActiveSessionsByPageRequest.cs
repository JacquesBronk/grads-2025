namespace Retro.Greeter.Contracts.Request;

public record GetActiveSessionsByPageRequest(int PageNumber = 1, int PageSize = 10);