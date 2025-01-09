namespace Retro.Greeter.Contracts.Request;

public record UpdateSessionRequest
(
    Guid Id,
    string UserId,
    long EntryEpoch,
    long ExitEpoch,
    string Route,
    string UserAgent,
    string IpAddress,
    bool IsActive
);