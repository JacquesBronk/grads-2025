using Microsoft.Extensions.Options;
using Retro.Configuration;
using Retro.Consul.HealthCheck;
using Retro.Yarp;
using Retro.Yarp.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddConfig("yarp");
builder.AddHeathCheckFor(["yarp"]);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddConfigBuilder();
var app = builder.Build();

var yarpConfigMonitor = app.Services.GetRequiredService<IOptionsMonitor<ReverseProxyConfig>>();
app.MapReverseProxy(proxyPipeline =>
{
    yarpConfigMonitor.OnChange(_ =>
    {
        proxyPipeline.UseRouting();
        proxyPipeline.UseEndpoints(endpoints => endpoints.MapReverseProxy());
    });
});

app.Run();