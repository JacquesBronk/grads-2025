using System.Text.Json;
using MongoDB.Driver;
using Retro.Domain;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Seeder.GreeterSeeder;

public class SessionSeederStrategy(IWebHostEnvironment environment, ILogger logger) : ISeedStrategy
{
    public async Task<Job> SeedAsync(CancellationToken cancellationToken = default)
    {
        Guid jobId = Guid.NewGuid();
        logger.LogInformation("Seeding Sessions");

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
                JobName = "Sessions",
                IsCompleted = false,
                Exception = new DirectoryNotFoundException("Seeding data not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        FileCache fileCache = new FileCache();
        fileCache.GetFiles(absolutePath);

        var sessionConfig = fileCache.ReadFileByName("greeter-api");

        if (string.IsNullOrWhiteSpace(sessionConfig))
        {
            logger.LogWarning("No greeter-api config found");
            return new Job
            {
                JobName = "Sessions",
                IsCompleted = false,
                Exception = new FileNotFoundException("greeter-api config not found"),
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        string dbConnection = GetConnectionString(sessionConfig, "Mongo");

        // Ensure Collection sessions exists if not, create it
        IMongoClientFactory mongoClientFactory = new MongoClientFactory(dbConnection);
        IMongoDbContext mongoDbContext = new MongoDbContext(mongoClientFactory, "hello-db-dev");
        var sessionCollection = mongoDbContext.GetCollection<Session>("sessions");

        // Count if ANY sessions exist do not generate fake data
        long count = await sessionCollection.CountDocumentsAsync(FilterDefinition<Session>.Empty, cancellationToken: cancellationToken);
        
        if (count > 0)
        {
            logger.LogInformation("Sessions already exist, skipping seeding");
            return new Job
            {
                JobName = "Sessions",
                IsCompleted = true,
                JobId = jobId,
                StartTime = DateTime.UtcNow
            };
        }

        // Generate fake sessions
        var sessions = SessionFaker.GenerateSessions(1000);

        // Insert into MongoDB
        await sessionCollection.InsertManyAsync(sessions, cancellationToken: cancellationToken);

        logger.LogInformation("Seeded 1000 sessions");

        return new Job
        {
            JobName = "Sessions",
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
        if (connectionStringsObj is JsonElement { ValueKind: JsonValueKind.Object } jsonElement)
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