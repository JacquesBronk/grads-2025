using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Retro;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Payments.Contracts.Request;
using Retro.Payments.Domain;
using Retro.Payments.Infrastructure;
using Retro.Persistence.Mongo;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "payments-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var paymentService = app.Services.GetRequiredService<IPaymentService>();

app.MapPost("/payments", async (CreatePaymentRequest createPaymentRequest) =>
{
    var paymentResponse = await paymentService.CreatePaymentAsync(createPaymentRequest, default);
    return Results.Created($"/payments/{paymentResponse.Id}", paymentResponse);
}).WithDescription("Create a new payment").WithName("CreatePayment");

app.MapGet("/payments/{paymentId}", async (Guid paymentId) =>
{
    var paymentResponse = await paymentService.GetPaymentAsync(paymentId, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Get a payment").WithName("GetPayment");

app.MapPut("/payments/{paymentId}/status", async (Guid paymentId, PaymentStatus paymentStatus) =>
{
    var paymentResponse = await paymentService.UpdatePaymentStatusAsync(paymentId, paymentStatus, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment status").WithName("UpdatePaymentStatus");

app.MapPut("/payments/{paymentId}/error", async (Guid paymentId, string error, string errorDescription, string errorReason, string errorReasonDescription, string errorReasonCode) =>
{
    var paymentResponse = await paymentService.UpdatePaymentErrorAsync(paymentId, error, errorDescription, errorReason, errorReasonDescription, errorReasonCode, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment error").WithName("UpdatePaymentError");

app.MapPut("/payments/{paymentId}/reference", async (Guid paymentId, string paymentReference) =>
{
    var paymentResponse = await paymentService.UpdatePaymentReferenceAsync(paymentId, paymentReference, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment reference").WithName("UpdatePaymentReference");

app.MapPut("/payments/{paymentId}/paid-at", async (Guid paymentId, DateTimeOffset paidAt) =>
{
    var paymentResponse = await paymentService.UpdatePaymentPaidAtAsync(paymentId, paidAt, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment paid at").WithName("UpdatePaymentPaidAt");

app.MapPut("/payments/{paymentId}/monies-paid", async (Guid paymentId, decimal moniesPaid) =>
{
    var paymentResponse = await paymentService.UpdatePaymentMoniesPaidAsync(paymentId, moniesPaid, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment monies paid").WithName("UpdatePaymentMoniesPaid");

app.MapPut("/payments/{paymentId}/monies-payable", async (Guid paymentId, decimal moniesPayable) =>
{
    var paymentResponse = await paymentService.UpdatePaymentMoniesPayableAsync(paymentId, moniesPayable, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment monies payable").WithName("UpdatePaymentMoniesPayable");

app.MapPut("/payments/{paymentId}/currency", async (Guid paymentId, string currency) =>
{
    var paymentResponse = await paymentService.UpdatePaymentCurrencyAsync(paymentId, currency, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment currency").WithName("UpdatePaymentCurrency");

app.MapPut("/payments/{paymentId}/method", async (Guid paymentId, string paymentMethod) =>
{
    var paymentResponse = await paymentService.UpdatePaymentMethodAsync(paymentId, paymentMethod, default);
    return paymentResponse is not null ? Results.Ok(paymentResponse) : Results.NotFound();
}).WithDescription("Update payment method").WithName("UpdatePaymentMethod");

app.UseServiceDiscovery();

app.Run();
