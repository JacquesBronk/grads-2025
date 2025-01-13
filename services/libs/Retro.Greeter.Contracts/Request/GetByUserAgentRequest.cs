using FastEndpoints;

namespace Retro.Greeter.Contracts.Request;

public record GetByUserAgentRequest
{
    [QueryParam, BindFrom("user-agent")]
    public string UserAgent { get; init; } = string.Empty;
    [BindFrom("pageNumber")]
    public int PageNumber { get; init; } = 1;
    [BindFrom("pageSize")]
    public int PageSize { get; init; } = 10;
}