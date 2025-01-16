using Retro.Cart.Contracts;
using Retro.Cart.Contracts.Request;
using Retro.Cart.Contracts.Response;

namespace Retro.Cart.Infrastructure;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<GetCartResponse> GetCart(Guid id, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCart(id, cancellationToken);
        
        return new GetCartResponse
        {
            Id = cart.Id,
            CartItems = cart.CartItems.Select(s => new CartItemDto
            {
                StockItemId = s.StockItemId,
                Quantity = s.Quantity
            }).ToList()
        };
    }

    public async Task<Guid> UpdateCart(UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var cart = new Domain.Cart
        {
            Id = request.Id ?? Guid.NewGuid(),
            CartItems = request.CartItems.Select(s => new Domain.CartItem
            {
                StockItemId = s.StockItemId,
                Quantity = s.Quantity
            }).ToList()
        };

        return await _cartRepository.UpdateCart(cart, cancellationToken);
    }
    
    public async Task<bool> RemoveCart(Guid id, CancellationToken cancellationToken)
    {
        return await _cartRepository.RemoveCart(id, cancellationToken);
    }
}