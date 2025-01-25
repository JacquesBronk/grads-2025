using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Infrastructure;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "ads-admin-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repo
builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<IAdService, AdService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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

app.MapGet("/ads/from-date", async (IAdService service, [FromQuery] DateTimeOffset fromDate, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, CancellationToken cancellationToken = default) =>
{
    var request = new GetAdDetailFromDate(fromDate, pageSize, pageNumber);
    return await service.GetAdDetailFromDate(request, cancellationToken);
});
   

app.MapGet("/ads/{id}", async (IAdService service, Guid id, CancellationToken cancellationToken) =>
    await service.GetAdDetailById(new GetAdDetailByIdRequest(Id: id), cancellationToken));

app.MapGet("/ads", async (IAdService service, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default) =>
    await service.GetAllAds(pageNumber, pageSize, cancellationToken));

app.MapGet("/ads/n", async (IAdService service, [FromQuery] int amount, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default) =>
{
    var request = new GetNAdDetailsRequest(amount, pageSize, pageNumber);
    return await service.GetNAds(request, cancellationToken);
});

app.MapGet("/ads/featured", async (IAdService service, [FromQuery] DateTimeOffset fromDate, [FromQuery] bool isActive = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default) =>
{
    var request = new GetFeaturedAdsRequest(fromDate, isActive, pageSize, pageNumber);
    return await service.GetFeatured(request, cancellationToken);
});

app.MapPost("/ads", async (IAdService service, [FromBody] UpsertAdRequest request, CancellationToken cancellationToken) =>
{
    await service.AddAdDetail(request, cancellationToken);
    return Results.Created($"/ads/{request.Id}", request);
});

app.MapPut("/ads", async (IAdService service, [FromBody] UpsertAdRequest request, CancellationToken cancellationToken) =>
{
    await service.UpdateAdDetail(request, cancellationToken);
    return Results.Ok(request);
});

app.MapDelete("/ads/{id}", async (IAdService service, Guid id, CancellationToken cancellationToken) =>
{
    await service.DeleteAdDetail(new DeleteAdDetailByIdRequest(Id: id), cancellationToken);
    return Results.NoContent();
});

app.UseCors();
app.UseServiceDiscovery();

app.Run();
