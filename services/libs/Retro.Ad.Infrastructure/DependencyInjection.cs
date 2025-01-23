using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Retro.Ad.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IAdsAdminApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetConnectionString("ads-admin-api")!));

        services.AddScoped<IAdMetricsRepository, AdMetricsRepository>();

        return services;
    }
}