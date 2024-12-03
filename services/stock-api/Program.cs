using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Configuration;
using Retro.Persistence.Mongo;
using Retro.Stock.Contracts.Request;
using Retro.Stock.Domain;
using Retro.Stock.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "stock-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repo
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, StockService>();

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

var stockService = app.Services.GetRequiredService<IStockService>();

app.MapGet("/api/stocks", async ([FromQuery] int pageNumber,[FromQuery] int pageSize,  CancellationToken cancellationToken) =>
{
    GetAllByPageRequest request = new(pageNumber, pageSize);
    return await stockService.GetAllAsync(request, cancellationToken);
}).WithDescription("Get all stocks with pagination").WithName("GetAllStocks");;

app.MapGet("/api/stocks/{id}", async (string id, CancellationToken cancellationToken) =>
{
    Guid.TryParse(id, out var stockId);
    return await stockService.GetByIdAsync(new GetByIdRequest(Id: stockId), cancellationToken);
}).WithDescription("Get stock by ID").WithName("GetStockById");;

app.MapGet("/api/stocks/sku/{sku}", async (string sku, CancellationToken cancellationToken) =>
{
    return await stockService.GetBySkuAsync(new GetBySkuRequest( Sku: sku ), cancellationToken);
}).WithDescription("Get stock by SKU").WithName("GetStockBySku");;

app.MapPost("/api/stocks", async (UpsertStockRequest request, CancellationToken cancellationToken) =>
{
    await stockService.UpsertStock(request, cancellationToken);
    return Results.NoContent();
}).WithDescription("Create or update a stock").WithName("UpsertStock");;

app.MapDelete("/api/stocks/{id}", async (string id, CancellationToken cancellationToken) =>
{
    Guid.TryParse(id, out var stockId);
    await stockService.DeleteAsync(new DeleteRequest(Id:stockId, String.Empty), cancellationToken);
    return Results.NoContent();
}).WithDescription("Delete stock by ID").WithName("DeleteStockById");;

app.MapDelete("/api/stocks/sku/{sku}", async (string sku, CancellationToken cancellationToken) =>
{
    await stockService.DeleteAsync(new DeleteRequest(Guid.Empty, sku), cancellationToken);
    return Results.NoContent();
}).WithDescription("Delete stock by SKU").WithName("DeleteStockBySku");;

app.MapGet("/api/stocks/search/condition", async ([FromQuery] int pageNumber,[FromQuery] int pageSize, [FromQuery] StockCondition condition, CancellationToken cancellationToken) =>
{
    GetByConditionRequest request = new(pageNumber, pageSize, condition);
    return await stockService.GetByConditionAsync(request, cancellationToken);
}).WithDescription("Search stocks by condition with pagination").WithName("SearchStocksByCondition");;

app.MapGet("/api/stocks/search/title", async ([FromQuery] int pageNumber,[FromQuery] int pageSize, [FromQuery] string title, CancellationToken cancellationToken) =>
{
    GetByTitleRequest request = new(title, pageNumber, pageSize);
    return await stockService.GetByTitleAsync(request, cancellationToken);
}).WithDescription("Search stocks by title with pagination").WithName("SearchStocksByTitle");

app.UseServiceDiscovery();


app.Run();
