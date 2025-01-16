namespace Retro.Cart.Contracts.Request;

public class UpdateCartRequest
{
    public Guid? Id { get; set; }

    public List<CartItemDto> CartItems { get; set; } = [];
}