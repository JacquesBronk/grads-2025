using System.Net.Http.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Retro.ServiceDiscovery;

public static class RegistrationExtensions
{
    public static Task RegisterServiceDiscovery(this WebApplicationBuilder builder)
    {
      IHttpClientFactory httpClientFactory = builder.Services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
        
        var consulAddress = Environment.GetEnvironmentVariable("CONSUL_ADDRESS")?.TrimEnd('/') ?? "http://localhost:8500";
        var client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(consulAddress);

        var service = builder.Configuration.GetSection("ServiceDetails");
        
        return client.PutAsJsonAsync($"/v1/agent/service/register", service.ToString());
    }
}