using Retro.Orders.Contracts.Response;

namespace Retro.Profile.Response;

public record ProfileResponse(Guid Id, string UserId, string UserName, string Email, OrderResponse[] Orders);