using FastEndpoints;

namespace Retro.Greeter.Contracts.Request;

public record GetByIpAddressRequest
{
    public string IpAddress { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}