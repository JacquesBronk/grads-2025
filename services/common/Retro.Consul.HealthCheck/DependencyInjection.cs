using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Retro.Consul.HealthCheck;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddHeathCheckFor(this WebApplicationBuilder builder, string[] services)
    {
        builder.Services.AddHttpClient();
        builder.Services.AddHealthChecks()
            .AddCheck<ConsulHealthCheck>("consul_health_check", 
            failureStatus: HealthStatus.Unhealthy,
            tags: new[] { "consul", "services" });

        return builder;
    }
}
