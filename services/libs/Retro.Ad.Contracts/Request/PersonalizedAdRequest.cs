namespace Retro.Ad.Contracts.Request;

public record PersonalizedAdRequest(
    string UserId,
    long UnixEpoch
);