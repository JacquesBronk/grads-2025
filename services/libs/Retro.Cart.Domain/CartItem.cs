using Retro.Stock.Domain;

namespace Retro.Cart.Domain;

public class CartItem
{
    public Guid StockItemId { get; set; }
    public int Quantity { get; set; }
}