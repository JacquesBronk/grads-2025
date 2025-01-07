namespace Retro.Greeter.Contracts.Response;

public record PagedSessionResponse(IEnumerable<SessionResponse> Items, int TotalCount, int PageNumber, int PageSize);