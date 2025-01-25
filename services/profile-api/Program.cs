using System.IdentityModel.Tokens.Jwt;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Retro;
using Retro.Cache.Redis;
using Retro.Configuration;
using Retro.Persistence.Mongo;
using Retro.Profile;

var builder = WebApplication.CreateBuilder(args);

const string serviceName = "profile-api";

builder.AddConfig(serviceName);
builder.AddMongoDbContext();
builder.AddServiceDiscovery();
builder.AddRedisCache();

var orderServiceAddress = builder.Configuration.GetConnectionString("order-api");

if (string.IsNullOrEmpty(orderServiceAddress))
{
    throw new Exception("Order service address is not configured.");
}

builder.Services.AddTransient(s => new Gateway(orderServiceAddress));

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddGlobalExceptionHandler();

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

app.MapGet("/profile/", async (IProfileService profileService, [FromQuery] Guid profileId, CancellationToken cancellationToken) =>
{
    var profile = await profileService.GetProfileByIdAsync(profileId, cancellationToken);

    return profile == null ? Results.NotFound() : Results.Ok(profile);
});

app.MapPost("/profile/", async (IProfileService profileService, Profile profile, CancellationToken cancellationToken) =>
{
    var createdProfile = await profileService.CreateProfileAsync(profile, cancellationToken);

    return Results.Created($"/profile/{createdProfile?.Id}", createdProfile);
});

app.MapPut("/profile/", async (IProfileService profileService, [FromQuery] Guid profileId, Profile profile, CancellationToken cancellationToken) =>
{
    var updatedProfile = await profileService.UpdateProfileAsync(profile, cancellationToken);

    return updatedProfile == null ? Results.NotFound() : Results.Ok(updatedProfile);
});

app.MapDelete("/profile/", async (IProfileService profileService, [FromQuery] Guid profileId, CancellationToken cancellationToken) =>
{
    var deletedProfile = await profileService.DeleteProfileAsync(profileId, cancellationToken);

    return deletedProfile == null ? Results.NotFound() : Results.Ok(deletedProfile);
});

app.MapGet("/profile/GetLoggedInUserProfile", async (IProfileService profileService, HttpContext httpContext, CancellationToken cancellationToken) =>
{
    var userId = httpContext.Request.Headers["X-UserId"];
    if (string.IsNullOrWhiteSpace(userId))
    {
        return Results.Unauthorized();
    }

    var profile = await profileService.GetProfileByUserIdAsync(userId!, cancellationToken);

    return profile == null ? Results.NotFound() : Results.Ok(profile);
});

app.MapGet("profile/GetKeyCloakUser", (HttpContext httpContext) =>
{
    var userName = httpContext.Request.Headers["X-UserName"];
    var userId = httpContext.Request.Headers["X-UserId"];
    
    if (string.IsNullOrWhiteSpace(userName))
    {
        return Results.Unauthorized();
    }
    
    var profile = new User{ UserName = userName.ToString(), UserId = userId.ToString() };
    return Results.Ok(profile);
});

app.UseGlobalExceptionHandler();

app.UseCors();
app.UseServiceDiscovery();

app.Run();
