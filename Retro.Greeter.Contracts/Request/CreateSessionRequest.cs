namespace Retro.Greeter.Contracts.Request;

/// <summary>
/// Create a new session
/// </summary>
/// <param name="UserId"></param>
/// <param name="EntryEpoch"></param>
/// <param name="ExitEpoch"></param>
/// <param name="Route"></param>
/// <param name="UserAgent"></param>
/// <param name="IpAddress"></param>
/// <param name="IsActive"></param>
public record CreateSessionRequest
(
    string UserId,
    long EntryEpoch,
    long ExitEpoch,
    string Route,
    string UserAgent,
    string IpAddress
);