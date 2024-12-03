using MongoDB.Driver;

namespace Retro.Persistence.Mongo.Infra;

public class MongoClientFactory(string connectionString): IMongoClientFactory
{
    private readonly IMongoClient _client = new MongoClient(connectionString);

    public IMongoClient GetClient()
    {
        return _client;
    }
}