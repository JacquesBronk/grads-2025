namespace Retro.Greeter.Contracts.Response;

public record SessionResponse
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