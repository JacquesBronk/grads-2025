using System.Text.Json;
using StackExchange.Redis;

namespace Retro.Cart.Infrastructure;

public class CartRepository: ICartRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private const string CartKey = "cart_";

    public CartRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<Domain.Cart> GetCart(Guid id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new TaskCanceledException();
        }

        var connected = _connectionMultiplexer.IsConnected;
        
        var cache = _connectionMultiplexer.GetDatabase();
        var key = $"{CartKey}_{id}";

        string? value = await cache.StringGetAsync(key);
        
        if (string.IsNullOrEmpty(value))
        {
            return new Domain.Cart
            {
                Id = id,
                CartItems = []
            };
        }
        
        return JsonSerializer.Deserialize<Domain.Cart>(value);
    }

    public async Task<Guid> UpdateCart(Domain.Cart cart, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new TaskCanceledException();
        }

        var key = $"{CartKey}_{cart.Id}";
    
        var cache = _connectionMultiplexer.GetDatabase();
        var value = JsonSerializer.Serialize(cart);
    
        await cache.StringSetAsync(key, value, TimeSpan.FromMinutes(5));

        return cart.Id;
    }
    
    public async Task<bool> RemoveCart(Guid id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            throw new TaskCanceledException();
        }
        
        try
        {
            var key = $"{CartKey}_{id}";
        
            var cache = _connectionMultiplexer.GetDatabase();
        
            await cache.KeyDeleteAsync(key);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}