using System.Net.Http.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Retro.Consul.HealthCheck;

public class ConsulHealthCheck(string[] serviceNames, IHttpClientFactory httpClientFactory)
    : IHealthCheck
{
    private readonly string _consulAddress = Environment.GetEnvironmentVariable("CONSUL_ADDRESS")?.TrimEnd('/') ?? "http://localhost:8500";

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        using var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_consulAddress);

        var unhealthyServices = new List<string>();
        var healthyServices = new List<string>();

        foreach (var serviceName in serviceNames)
        {
            try
            {
                var response = await client.GetAsync($"/v1/health/service/{serviceName}?passing", cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    unhealthyServices.Add(serviceName);
                    continue;
                }

                var serviceInstances = await response.Content.ReadFromJsonAsync<List<ConsulHealthResponse>>(cancellationToken: cancellationToken);

                if (serviceInstances?.Any() == true)
                {
                    healthyServices.Add(serviceName);
                }
                else
                {
                    unhealthyServices.Add(serviceName);
                }
            }
            catch (Exception ex)
            {
                unhealthyServices.Add($"{serviceName} (error: {ex.Message})");
            }
        }

        if (unhealthyServices.Any())
        {
            return HealthCheckResult.Unhealthy(
                "One or more services are unhealthy.",
                data: new Dictionary<string, object>
                {
                    ["UnhealthyServices"] = unhealthyServices,
                    ["HealthyServices"] = healthyServices
                });
        }

        return HealthCheckResult.Healthy("All services are healthy.");
    }
}
