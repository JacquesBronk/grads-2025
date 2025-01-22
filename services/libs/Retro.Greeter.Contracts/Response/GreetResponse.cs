using Retro.Ad.Contracts.Response;

namespace Retro.Greeter.Contracts.Response;

public record GreetResponse
{
    public string Message { get; init; } = string.Empty;
    public string? SignupSpecial { get; init; }
    public List<AdResponse> Ads { get; init; } = [];
};