using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Greeter.Infrastructure;
using Retro.Persistence.Mongo;
using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "greeter-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddFastEndpoints()
                .AddSwaggerDocument(options =>
                {
                    options.Title = serviceName;
                    options.Version = "v1";
                    options.Description = "Retro Greeter API";
                });
builder.Services.AddEndpointsApiExplorer();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Handlers
builder.Services.AddGlobalExceptionHandler();

var app = builder.Build();

app.UseFastEndpoints()
   .UseSwaggerGen();

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

app.UseCors();
app.UseServiceDiscovery();

app.Run();