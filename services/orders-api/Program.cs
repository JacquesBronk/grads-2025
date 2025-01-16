using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Orders.Contracts;
using Retro.Orders.Contracts.Request;
using Retro.Orders.Infrastructure;
using Retro.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "order-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

var stockServiceAddress = builder.Configuration.GetConnectionString("stock-api");

if (string.IsNullOrEmpty(stockServiceAddress))
{
    throw new Exception("Stock service address is not configured.");
}

builder.Services.AddTransient(s => new Gateway(stockServiceAddress));

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

app.MapGet("/order/{id}", async (IOrderService orderService, [FromRoute]Guid id, CancellationToken cancellationToken) =>
{
    var cart = await orderService.GetOrderByIdAsync(id, cancellationToken);
    return Results.Ok(cart);
}).WithDescription("Get an order by id").WithName("GetOrder");

app.MapGet("/order/GetByUserId", async (IOrderService orderService, [FromQuery]Guid id, CancellationToken cancellationToken) =>
{
    var cart = await orderService.GetUserOrdersAsync(id, cancellationToken);
    return Results.Ok(cart);
}).WithDescription("Get orders for user by id").WithName("GetByUserId");

app.MapPost("/order/create", async (IOrderService orderService, [FromBody]CreateOrderRequest request, CancellationToken cancellationToken) =>
{
    var result = await orderService.CreateOrderAsync(request, cancellationToken);
    return Results.Ok(result);
}).WithDescription("Create an order").WithName("CreateOrder");

app.MapPatch("/order/complete", async (IOrderService orderService, [FromBody]CompleteOrderRequest request, CancellationToken cancellationToken) =>
{
    var result = await orderService.CompleteOrderAsync(request, cancellationToken);
    return Results.Ok(result);
}).WithDescription("Complete order").WithName("CompleteOrder");

app.MapPatch("/order/cancel/{id}", async (IOrderService orderService, [FromRoute]Guid id, CancellationToken cancellationToken) =>
{
    var result = await orderService.CancelOrderAsync(id, cancellationToken);
    return Results.Ok(result);
}).WithDescription("Cancel order").WithName("CancelOrder");


app.UseServiceDiscovery();
app.Run();