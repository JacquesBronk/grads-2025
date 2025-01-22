using Retro.Orders.Contracts.Response;

namespace Retro.Profile;

public record ProfileResponse(Guid Id, string UserName, OrderResponse[] Orders);