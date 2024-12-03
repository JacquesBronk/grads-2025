using MongoDB.Driver;
using Retro.Persistence.Mongo.Infra;
using Retro.ResultWrappers;
using Retro.Stock.Domain;

namespace Retro.Stock.Infrastructure;

public class StockRepository(IMongoDbContext mongoDbContext) : IStockRepository
{
    private readonly IMongoCollection<StockItem> _collection = mongoDbContext.GetCollection<StockItem>("stock_items");

    public async Task<PaginatedResult<StockItem>> GetAllAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(FilterDefinition<StockItem>.Empty,
            cancellationToken: cancellationToken);

        var items = await _collection
            .Find(FilterDefinition<StockItem>.Empty)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<StockItem>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<StockItem> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<StockItem> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(s => s.Sku == sku).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        if (stockItem.Id == Guid.Empty)
        {
            stockItem.Id = Guid.NewGuid();
        }
        
        await _collection.InsertOneAsync(stockItem, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(StockItem stockItem, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(s => s.Id == stockItem.Id, stockItem, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(s => s.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(string sku, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(s => s.Sku == sku, cancellationToken);
    }

    public async Task<PaginatedResult<StockItem>> GetByConditionAsync(int pageNumber, int pageSize,StockCondition condition,
        CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(s => s.Condition == condition, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(s => s.Condition == condition)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<StockItem>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<StockItem>> GetByTitleAsync(string title, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0) pageSize = 10;
        if (pageNumber <= 0) pageNumber = 1;

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = (int)await _collection.CountDocumentsAsync(s => s.Title.Contains(title), cancellationToken: cancellationToken);

        var items = await _collection
            .Find(s => s.Title.Contains(title))
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<StockItem>(items, totalCount, pageNumber, pageSize);
    }
}