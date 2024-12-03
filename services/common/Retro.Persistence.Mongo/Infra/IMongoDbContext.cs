using MongoDB.Driver;

namespace Retro.Persistence.Mongo.Infra;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string collectionName);

    IMongoCollection<T> CreateCollection<T>(string collectionName);
}