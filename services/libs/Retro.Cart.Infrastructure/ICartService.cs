using Retro.Cart.Contracts.Request;
using Retro.Cart.Contracts.Response;

namespace Retro.Cart.Infrastructure;

public interface ICartService
{
    Task<GetCartResponse> GetCart(Guid id, CancellationToken cancellationToken);
    Task<Guid> UpdateCart(UpdateCartRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveCart(Guid id, CancellationToken cancellationToken);
}