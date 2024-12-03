using MongoDB.Driver;

namespace Retro.Persistence.Mongo.Infra;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClientFactory clientFactory, string dbName)
    {
        var client = clientFactory.GetClient();
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public IMongoCollection<T> CreateCollection<T>(string collectionName)
    {
        _database.CreateCollection(collectionName);

        return _database.GetCollection<T>(collectionName);
    }
}