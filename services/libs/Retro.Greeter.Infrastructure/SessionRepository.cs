using MongoDB.Driver;
using Retro.Domain;
using Retro.Persistence.Mongo.Infra;
using Retro.ResultWrappers;

namespace Retro.Greeter.Infrastructure;

public class SessionRepository(IMongoDbContext mongoDbContext) : ISessionRepository
{
    private readonly IMongoCollection<Session> _collection = mongoDbContext.GetCollection<Session>("sessions");
    public async Task<PaginatedResult<Session>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        var skip = (pageNumber - 1) * pageSize;
        var totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<Session>.Empty, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(FilterDefinition<Session>.Empty)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Session>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<Session>> GetActiveSessionsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        var skip = (pageNumber - 1) * pageSize;
        var totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<Session>.Empty, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(s => s.IsActive)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Session>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<Session> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _collection.Find(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<Session> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default) => 
        await _collection.Find(s => s.UserId == userId).FirstOrDefaultAsync(cancellationToken);

    public async Task<Session> GetByRouteAsync(string route, CancellationToken cancellationToken = default) => 
        await _collection.Find(s => s.Route == route).FirstOrDefaultAsync(cancellationToken);

    public async Task<Session> GetByUserAgentAsync(string userAgent, CancellationToken cancellationToken = default) => 
        await _collection.Find(s => s.UserAgent == userAgent).FirstOrDefaultAsync(cancellationToken);

    public async Task<Session> GetByIpAddressAsync(string ipAddress, CancellationToken cancellationToken = default) => 
        await _collection.Find(s => s.IpAddress == ipAddress).FirstOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(Session session, CancellationToken cancellationToken = default)
    {
        if (session.Id == Guid.Empty)
        {
            session.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(session, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken = default) =>
        await _collection.ReplaceOneAsync(s => s.Id == session.Id, session, cancellationToken: cancellationToken);

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _collection.DeleteOneAsync(s => s.Id == id, cancellationToken);
}