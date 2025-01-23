using System.Text.Json;
using Retro.Ad.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Seeder.AdSeeder;

public class AdViewMetricSeederStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding ad view metrics");

        string absolutePath = Path.Combine("seed", "consul");
        if (environment.IsDevelopment())
        {
            absolutePath = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "..", "..", "init"));
        }

        if (!Directory.Exists(absolutePath))
        {
            logger.LogWarning("No seed data found at {AbsolutePath}", absolutePath);
            return new Job
            {
                JobName = "ads",
                IsCompleted = false,
                Exception = new DirectoryNotFoundException("Seeding data not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        FileCache fileCache = new FileCache();
        fileCache.GetFiles(absolutePath);

        var adViewConfig = fileCache.ReadFileByName("ads-api");

        if (string.IsNullOrWhiteSpace(adViewConfig))
        {
            logger.LogWarning("No ads-api config found");
            return new Job
            {
                JobName = "ads",
                IsCompleted = false,
                Exception = new FileNotFoundException("ads-api config not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        string dbConnection = GetConnectionString(adViewConfig, "Mongo");

        //Ensure Collection ad_view_metrics exists if not, create it
        IMongoClientFactory mongoClientFactory = new MongoClientFactory(dbConnection);
        IMongoDbContext mongoDbContext = new MongoDbContext(mongoClientFactory, "ads-db-dev");
        var adDetailCollection = mongoDbContext.GetCollection<AdViewMetric>("ad-metrics");

        //Count if ANY ad_view_metrics exist do not generate fake data
        long count = await adDetailCollection.CountDocumentsAsync(MongoDB.Driver.FilterDefinition<AdViewMetric>.Empty, cancellationToken: cancellationToken);
        
        if (count > 0)
        {
            logger.LogInformation("Ad View Metrics already exist, skipping seeding");
            return new Job
            {
                JobName = "ad-view-metrics",
                IsCompleted = true,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }
        
        // Generate fake stock items
        var adViewMetrics = AdViewMetricFaker.GenerateAdMetrics(100);

        // Insert into MongoDB
        await adDetailCollection.InsertManyAsync(adViewMetrics, cancellationToken: cancellationToken);

        logger.LogInformation("Seeded 100 ad view metrics");

        return new Job
        {
            JobName = "ads",
            IsCompleted = true,
            JobId = jobId,
            StartTime = DateTime.UtcNow
        };
    }
    
    private string GetConnectionString(string adViewConfig, string name)
    {
        var config = JsonSerializer.Deserialize<Dictionary<string, object>>(adViewConfig);

        if (config == null || !config.TryGetValue("ConnectionStrings", out var connectionStringsObj))
        {
            throw new InvalidOperationException("ConnectionStrings section not found in ads-api config");
        }

        // Handle if ConnectionStrings is a string
        if (connectionStringsObj is string connectionStringPlain)
        {
            if (name == "Mongo")
            {
                return connectionStringPlain;
            }
            throw new InvalidOperationException($"Connection string {name} not found in ads-api config");
        }

        // Handle if ConnectionStrings is a dictionary
        if (connectionStringsObj is JsonElement { ValueKind: JsonValueKind.Object } jsonElement)
        {
            var connectionStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonElement.GetRawText());
            if (connectionStrings != null && connectionStrings.TryGetValue(name, out var connectionString))
            {
                return connectionString;
            }
        }

        throw new InvalidOperationException($"Connection string {name} not found in ads-api config");
    }
}