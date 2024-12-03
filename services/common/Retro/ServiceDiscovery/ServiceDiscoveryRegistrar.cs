using System.Net;
using System.Net.Http.Json;
using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Retro.ServiceDiscovery;

public class ServiceDiscoveryRegistrar(
    IOptionsMonitor<ServiceDetails> configuration,
    IHostEnvironment environment,
    ILogger<ServiceDiscoveryRegistrar> logger)
    : IServiceDiscoveryRegistrar
{
    public async Task RegisterServiceAsync()
    {
        string consulAddress = environment.IsDevelopment() ? "http://localhost:8500" : "http://consul:8500";

        ConsulClient client = new ConsulClient(config => config.Address = new Uri(consulAddress));


        var serviceDetails = configuration.CurrentValue;

        serviceDetails.Check.Interval = serviceDetails.Check.Interval == 0 ? 10 : serviceDetails.Check.Interval;
        serviceDetails.Check.Timeout = serviceDetails.Check.Timeout == 0 ? 5 : serviceDetails.Check.Timeout;
        
        logger.LogInformation("Registering service {serviceDetails}", serviceDetails);
        logger.LogInformation("ServiceName: {ServiceName}", serviceDetails.Name);
        
        var registration = new AgentServiceRegistration
        {
            ID = serviceDetails.ID,
            Name = serviceDetails.Name,
            Address = serviceDetails.Address,
            Port = serviceDetails.Port,
            Tags = serviceDetails.Tags?.ToArray(),
            Check = new AgentServiceCheck
            {
                HTTP = serviceDetails.Check.HTTP,
                Interval = TimeSpan.FromSeconds(serviceDetails.Check.Interval),
                Timeout = TimeSpan.FromSeconds(serviceDetails.Check.Timeout),
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
            }
        };

        if (serviceDetails == null)
        {
            throw new InvalidOperationException("Service details not configured properly.");
        }

        var response = await client.Agent.ServiceRegister(registration);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException($"Failed to register service: {response.StatusCode}");
        }
    }
}