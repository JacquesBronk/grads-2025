using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Retro.Cache.Redis;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddRedisCache(this WebApplicationBuilder builder)
    {
        string? redisConnectionString = builder.Configuration.GetConnectionString("Redis");

        if (redisConnectionString is null)
        {
            throw new ArgumentNullException(nameof(AddRedisCache), "Redis connection string is not found in the configuration");
        }
        builder.Services.AddSingleton<IConnectionMultiplexer>(options => ConnectionMultiplexer.Connect(redisConnectionString));
        
        return builder;
    }
}