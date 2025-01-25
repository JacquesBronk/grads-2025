using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Retro.Greeter.Infrastructure.Interfaces;
using Retro.Greeter.Infrastructure.Services;

namespace Retro.Greeter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<IAdsApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetConnectionString("ads-api")!));

        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IAdService, AdService>();

        return services;
    }
}