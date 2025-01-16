namespace Retro.Greeter.Contracts.Request;

/// <summary>
/// Delete a session by id
/// </summary>
/// <param name="Id"></param>
public record DeleteSessionRequest(Guid Id);