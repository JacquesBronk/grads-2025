using MongoDB.Driver;
using Retro.Orders.Contracts;
using Retro.Orders.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Orders.Infrastructure;

public class OrderRepository(IMongoDbContext mongoDbContext) : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection = mongoDbContext.GetCollection<Order>("stock_items");

    public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await _collection.Find(s => s.Id == orderId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Order[]> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        return (await _collection
            .Find(s => s.UserId == userId)
            .ToListAsync(cancellationToken))
            .ToArray();
    }

    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(order, new InsertOneOptions() {  }, cancellationToken: cancellationToken);

        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(s => s.Id == order.Id, order, cancellationToken: cancellationToken);

        return order;
    }
}