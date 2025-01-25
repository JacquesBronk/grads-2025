namespace Retro.Cart.Contracts.Response;

public class GetCartResponse
{
    public Guid Id { get; set; }

    public List<CartItemDto> CartItems { get; set; } = [];
}