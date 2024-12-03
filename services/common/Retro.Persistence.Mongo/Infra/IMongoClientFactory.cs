using MongoDB.Driver;

namespace Retro.Persistence.Mongo.Infra;

public interface IMongoClientFactory
{
    IMongoClient GetClient();
}
