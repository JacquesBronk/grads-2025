using FastEndpoints;

namespace Retro.Greeter.Contracts.Request;

public record GetByRouteRequest
{
    [QueryParam, BindFrom("route")]
    public string Route { get; init; } = string.Empty;
    [BindFrom("pageNumber")]
    public int PageNumber { get; init; } = 1;
    [BindFrom("pageSize")]
    public int PageSize { get; init; } = 10;
}