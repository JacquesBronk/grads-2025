namespace Retro.Cart.Infrastructure;

public interface ICartRepository
{
    Task<Domain.Cart> GetCart(Guid id, CancellationToken cancellationToken);
    Task<Guid> UpdateCart(Domain.Cart cart, CancellationToken cancellationToken);
    Task<bool> RemoveCart(Guid id, CancellationToken cancellationToken);
}