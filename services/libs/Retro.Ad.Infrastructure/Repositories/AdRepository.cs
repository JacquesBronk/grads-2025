using MongoDB.Driver;
using Retro.Ad.Domain;
using Retro.Persistence.Mongo.Infra;
using Retro.ResultWrappers;

namespace Retro.Ad.Infrastructure;

public class AdRepository(IMongoDbContext mongoDbContext): IAdRepository
{
    private readonly IMongoCollection<AdDetail> _collection = mongoDbContext.GetCollection<AdDetail>("ads");
    
    public async Task<AdDetail> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(a => a.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginatedResult<AdDetail>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<AdDetail>.Empty, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(FilterDefinition<AdDetail>.Empty)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AdDetail>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<AdDetail>> GetAdDetailFromDate(DateTimeOffset dateTimeOffset,int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<AdDetail>.Empty, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(a => a.StartDateTime >= dateTimeOffset)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AdDetail>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<AdDetail>> GetNAd(int amount, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<AdDetail>.Empty, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(FilterDefinition<AdDetail>.Empty)
            .Skip(skip)
            .Limit(amount)
            .SortByDescending(a => a.CreatedDateTime)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AdDetail>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<AdDetail>> GetFeatured(DateTimeOffset fromDate = default, bool isActive = true, int pageSize = 10, int pageNumber = 1,
        CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<AdDetail>.Empty, cancellationToken: cancellationToken);
        
        var items = await _collection
            .Find(a => a.IsActive == isActive && a.IsFeatured && a.StartDateTime >= fromDate)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<AdDetail>(items, totalCount, pageNumber, pageSize);
    }

    public async Task AddAsync(AdDetail adDetail, CancellationToken cancellationToken = default)
    {
        if (adDetail.Id == Guid.Empty)
        {
            adDetail.Id = Guid.NewGuid();
        }

        await _collection.InsertOneAsync(adDetail, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(AdDetail adDetail, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(a => a.Id == adDetail.Id, adDetail, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(a => a.Id == id, cancellationToken);
    }
}