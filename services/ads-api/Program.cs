using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Ad.Infrastructure;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "ads-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints()
                .SwaggerDocument();


// Repo
builder.Services.AddScoped<IAdService, AdService>();
builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<IAdMetricsRepository, AdMetricsRepository>();

// Services
builder.Services.AddInfrastructure(builder.Configuration);

// Handlers
builder.Services.AddGlobalExceptionHandler();

var app = builder.Build();

app.UseFastEndpoints(options =>
    {
        options.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .UseSwaggerGen(uiConfig: settings =>
    {
        settings.DocumentPath = $"/{serviceName}/swagger/{{documentName}}/swagger.json";
    });

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            results = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                data = e.Value.Data
            })
        };
        await context.Response.WriteAsJsonAsync(result);
    }
});

app.UseGlobalExceptionHandler();

app.UseServiceDiscovery();

app.Run();