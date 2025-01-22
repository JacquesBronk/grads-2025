using MongoDB.Driver;
using Retro.Domain;
using Retro.Persistence.Mongo.Infra;
using Retro.ResultWrappers;

namespace Retro.Greeter.Infrastructure;

public class SessionRepository(IMongoDbContext mongoDbContext) : ISessionRepository
{
    private readonly IMongoCollection<Session> _collection = mongoDbContext.GetCollection<Session>("sessions");
    public async Task<PaginatedResult<Session>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Empty, pageNumber, pageSize, cancellationToken);

    public async Task<PaginatedResult<Session>> GetActiveSessionsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Eq(s => s.IsActive, true), pageNumber, pageSize, cancellationToken);

    public async Task<Session> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _collection.Find(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);
    
    public async Task<PaginatedResult<Session>> GetByUserIdAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Eq(s => s.UserId, userId), pageNumber, pageSize, cancellationToken);

    public async Task<PaginatedResult<Session>> GetByRouteAsync(string route, int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Eq(s => s.Route, route), pageNumber, pageSize, cancellationToken);

    public async Task<PaginatedResult<Session>> GetByUserAgentAsync(string userAgent, int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Eq(s => s.UserAgent, userAgent), pageNumber, pageSize, cancellationToken);

    public async Task<PaginatedResult<Session>> GetByIpAddressAsync(string ipAddress, int pageNumber, int pageSize, CancellationToken cancellationToken = default) =>
        await GetPagedResultAsync(Builders<Session>.Filter.Eq(s => s.IpAddress, ipAddress), pageNumber, pageSize, cancellationToken);

    public async Task<Session> CreateAsync(Session session, CancellationToken cancellationToken = default)
    {
        if (session.Id == Guid.Empty)
        {
            session.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(session, cancellationToken: cancellationToken);
        
        return session;
    }

    public async Task<Session> UpdateAsync(Session session, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(session.Id.ToString(), nameof(session.Id));

        var updatedSession = await _collection.FindOneAndReplaceAsync(
            s => s.Id == session.Id,
            session,
            new FindOneAndReplaceOptions<Session> { ReturnDocument = ReturnDocument.After },
            cancellationToken
        );

        return updatedSession;
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _collection.DeleteOneAsync(s => s.Id == id, cancellationToken);
    
    private async Task<PaginatedResult<Session>> GetPagedResultAsync(FilterDefinition<Session> filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        var skip = (pageNumber - 1) * pageSize;
        var totalCount = (int)await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(filter)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<Session>(items, totalCount, pageNumber, pageSize);
    }
}