using MongoDB.Driver;
using Retro.Ad.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Ad.Infrastructure;

public class AdMetricsRepository(IMongoDbContext mongoDbContext) : IAdMetricsRepository
{
    private readonly IMongoCollection<AdViewMetric> _collection = mongoDbContext.GetCollection<AdViewMetric>("ad-metrics");
    public async Task TrackAdViewAsync(AdViewMetric metric, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(metric, cancellationToken: cancellationToken);
    }
    
    public async Task<IEnumerable<Guid>> GetViewedAdIdsAsync(string userId, CancellationToken cancellationToken)
    {
        var filter = Builders<AdViewMetric>.Filter.Eq(metric => metric.UserId, userId);
        var metrics = await _collection.Find(filter).ToListAsync(cancellationToken);
        return metrics.Select(metric => metric.AdId);
    }
}