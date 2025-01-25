using Retro.Orders.Contracts.Response;

namespace Retro.Profile;

public record ProfileResponse(Guid Id, string UserId, string UserName, string Email, OrderResponse[] Orders);