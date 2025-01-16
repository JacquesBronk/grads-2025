using FastEndpoints;

namespace Retro.Greeter.Contracts.Request;

public record GetByUserIdRequest
{
    [QueryParam, BindFrom("user-id")]
    public string UserId { get; init; } = string.Empty;
    [BindFrom("pageNumber")]
    public int PageNumber { get; init; } = 1;
    [BindFrom("pageSize")]
    public int PageSize { get; init; } = 10;
}