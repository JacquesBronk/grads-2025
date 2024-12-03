using System.Text.Json;
using MongoDB.Driver;
using Retro.Persistence.Mongo.Infra;
using Retro.Stock.Domain;

namespace Retro.Seeder.StockSeeder;

public class StockSeederStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding Keycloak");

        string absolutePath = Path.Combine("seed", "consul");
        if (environment.IsDevelopment())
        {
            absolutePath = Path.GetFullPath(Path.Combine(environment.ContentRootPath, "..", "..", "init"));
        }

        if (!Directory.Exists(absolutePath))
        {
            logger.LogWarning($"No seed data found at {absolutePath}");
            return new Job
            {
                JobName = "Keycloak",
                IsCompleted = false,
                Exception = new DirectoryNotFoundException("Seeding data not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        FileCache fileCache = new FileCache();
        fileCache.GetFiles(absolutePath);

        var stockConfig = fileCache.ReadFileByName("stock-api");

        if (string.IsNullOrWhiteSpace(stockConfig))
        {
            logger.LogWarning("No stock-api config found");
            return new Job
            {
                JobName = "Keycloak",
                IsCompleted = false,
                Exception = new FileNotFoundException("stock-api config not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        string dbConnection = GetConnectionString(stockConfig, "Mongo");

        //Ensure Collection stock_items exists if not, create it
        IMongoClientFactory mongoClientFactory = new MongoClientFactory(dbConnection);
        IMongoDbContext mongoDbContext = new MongoDbContext(mongoClientFactory, "stock-db-dev");
        var stockItemsCollection = mongoDbContext.GetCollection<StockItem>("stock_items");

        if (stockItemsCollection == null)
        {
            mongoDbContext.CreateCollection<StockItem>("stock_items");
            stockItemsCollection = mongoDbContext.GetCollection<StockItem>("stock_items");
        }
        
        //Count if ANY stock items exist do not generate fake data
        long count = await stockItemsCollection.CountDocumentsAsync(FilterDefinition<StockItem>.Empty, cancellationToken: cancellationToken);
        
        if (count > 0)
        {
            logger.LogInformation("Stock items already exist, skipping seeding.");
            return new Job
            {
                JobName = "StockSeeder",
                IsCompleted = true,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }
        

        // Generate fake stock items
        var stockItems = StockItemFaker.GenerateStockItems(1000);

        // Insert into MongoDB
        await stockItemsCollection.InsertManyAsync(stockItems, cancellationToken: cancellationToken);

        logger.LogInformation("Seeded 1000 stock items.");

        return new Job
        {
            JobName = "StockSeeder",
            IsCompleted = true,
            JobId = jobId,
            StartTime = DateTime.UtcNow
        };
    }

    private string GetConnectionString(string stockConfig, string name)
    {
        var config = JsonSerializer.Deserialize<Dictionary<string, object>>(stockConfig);

        if (config == null || !config.TryGetValue("ConnectionStrings", out var connectionStringsObj))
        {
            throw new InvalidOperationException("ConnectionStrings section not found in stock-api config");
        }

        // Handle if ConnectionStrings is a string
        if (connectionStringsObj is string connectionStringPlain)
        {
            if (name == "Mongo")
            {
                return connectionStringPlain;
            }
            throw new InvalidOperationException($"Connection string {name} not found in stock-api config");
        }

        // Handle if ConnectionStrings is a dictionary
        if (connectionStringsObj is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
        {
            var connectionStrings = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonElement.GetRawText());
            if (connectionStrings != null && connectionStrings.TryGetValue(name, out var connectionString))
            {
                return connectionString;
            }
        }

        throw new InvalidOperationException($"Connection string {name} not found in stock-api config");
    }
}