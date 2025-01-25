using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Cache.Redis;
using Retro.Cart.Contracts.Request;
using Retro.Cart.Infrastructure;
using Retro.Configuration;
using Retro.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "cart-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICartRepository, CartRepository>();

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



app.MapGet("/cart/{id}", async (ICartService cartService, [FromRoute]Guid id, CancellationToken cancellationToken) =>
{
    var cart = await cartService.GetCart(id, cancellationToken);
    return Results.Ok(cart);
}).WithDescription("Get a cart by id").WithName("GetCart");

app.MapPost("/cart/update", async (ICartService cartService, [FromBody]UpdateCartRequest request, CancellationToken cancellationToken) =>
{
    var result = await cartService.UpdateCart(request, cancellationToken);
    return Results.Ok(result);
}).WithDescription("Update a cart").WithName("UpdateCart");

app.MapDelete("/cart/{id}", async (ICartService cartService, [FromRoute]Guid id, CancellationToken cancellationToken) =>
{
    var result = await cartService.RemoveCart(id, cancellationToken);
    return Results.Ok(result);
}).WithDescription("Remove a cart by id").WithName("RemoveCart");

app.UseCors();
app.UseServiceDiscovery();
app.Run();
