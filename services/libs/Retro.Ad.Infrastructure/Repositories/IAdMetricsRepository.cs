using Retro.Ad.Domain;

namespace Retro.Ad.Infrastructure;

public interface IAdMetricsRepository
{
    Task TrackAdViewAsync(AdViewMetric metric, CancellationToken cancellationToken);
    Task<IEnumerable<Guid>> GetViewedAdIdsAsync(string userId, CancellationToken cancellationToken);
}