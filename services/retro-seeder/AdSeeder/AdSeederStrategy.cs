using System.Text.Json;
using MongoDB.Driver;
using Retro.Ad.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Seeder.AdSeeder;

public class AdSeederStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding ads");

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

        var stockConfig = fileCache.ReadFileByName("ads-admin-api");

        if (string.IsNullOrWhiteSpace(stockConfig))
        {
            logger.LogWarning("No ads-admin-api config found");
            return new Job
            {
                JobName = "ads",
                IsCompleted = false,
                Exception = new FileNotFoundException("ads-admin-api config not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        string dbConnection = GetConnectionString(stockConfig, "Mongo");

        //Ensure Collection stock_items exists if not, create it
        IMongoClientFactory mongoClientFactory = new MongoClientFactory(dbConnection);
        IMongoDbContext mongoDbContext = new MongoDbContext(mongoClientFactory, "ads-db-dev");
        var adDetailCollection = mongoDbContext.GetCollection<AdDetail>("ads");

        //Count if ANY stock items exist do not generate fake data
        long count = await adDetailCollection.CountDocumentsAsync(FilterDefinition<AdDetail>.Empty, cancellationToken: cancellationToken);
        
        if (count > 0)
        {
            logger.LogInformation("Stock items already exist, skipping seeding");
            return new Job
            {
                JobName = "ads",
                IsCompleted = true,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }
        
        // Generate fake stock items
        var stockItems = AdDetailFaker.GenerateAdDetails(100);

        // Insert into MongoDB
        await adDetailCollection.InsertManyAsync(stockItems, cancellationToken: cancellationToken);

        logger.LogInformation("Seeded 100 ads");

        return new Job
        {
            JobName = "ads",
            IsCompleted = true,
            JobId = jobId,
            StartTime = DateTime.UtcNow
        };
    }
    
    private string GetConnectionString(string adConfig, string name)
    {
        var config = JsonSerializer.Deserialize<Dictionary<string, object>>(adConfig);

        if (config == null || !config.TryGetValue("ConnectionStrings", out var connectionStringsObj))
        {
            throw new InvalidOperationException("ConnectionStrings section not found in ads-admin-api config");
        }

        // Handle if ConnectionStrings is a string
        if (connectionStringsObj is string connectionStringPlain)
        {
            if (name == "Mongo")
            {
                return connectionStringPlain;
            }
            throw new InvalidOperationException($"Connection string {name} not found in ads-admin-api config");
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

        throw new InvalidOperationException($"Connection string {name} not found in ads-admin-api config");
    }
}