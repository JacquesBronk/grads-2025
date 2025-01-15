using Retro.Ad.Contracts.Response;

namespace Retro.Greeter.Contracts.Response;

public record GreetResponse
{
    public string Message { get; init; }
    public string? SignupSpecial { get; init; }
    // TODO: Create Ads API integration and populate this list
    public List<AdDetailResponse> Ads { get; init; }
};