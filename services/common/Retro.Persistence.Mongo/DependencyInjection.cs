using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Retro.Persistence.Mongo.Infra;

namespace Retro.Persistence.Mongo;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddMongoDbContext(this WebApplicationBuilder builder)
    {
        
        string? mongoDbConnectionString = builder.Configuration.GetConnectionString("Mongo");
        string? databaseName = builder.Configuration["Mongo:Database"];

        if (string.IsNullOrWhiteSpace(mongoDbConnectionString))
        {
            throw new MongoConfigurationException("The MongoDb connection string is not configured.");
        }
        
        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new MongoConfigurationException("The MongoDb database name is not configured.");
        }
        
        builder.Services.AddSingleton<IMongoClientFactory>(_ =>
            new MongoClientFactory(mongoDbConnectionString));
        
        builder.Services.AddScoped<IMongoDbContext>(sp =>
            new MongoDbContext(sp.GetRequiredService<IMongoClientFactory>(), databaseName));
        
        return builder;
    }
}