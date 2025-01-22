using System.IdentityModel.Tokens.Jwt;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

// Configure Keycloak authentication
// JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
// builder.Services.AddAuthorization(s => s.AddPolicy("auth-policy", policyBuilder => policyBuilder.RequireAuthenticatedUser()));

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseAuthentication();
// app.UseAuthorization();

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

app.MapGet("/profile/{profileId}", async (IProfileService profileService, Guid profileId, CancellationToken cancellationToken) =>
{
    var profile = await profileService.GetProfileByIdAsync(profileId, cancellationToken);

    return profile == null ? Results.NotFound() : Results.Ok(profile);
});

app.MapPost("/profile", async (IProfileService profileService, Profile profile, CancellationToken cancellationToken) =>
{
    var createdProfile = await profileService.CreateProfileAsync(profile, cancellationToken);

    return Results.Created($"/profile/{createdProfile?.Id}", createdProfile);
});

app.MapPut("/profile/{profileId}", async (IProfileService profileService, Guid profileId, Profile profile, CancellationToken cancellationToken) =>
{
    var updatedProfile = await profileService.UpdateProfileAsync(profile, cancellationToken);

    return updatedProfile == null ? Results.NotFound() : Results.Ok(updatedProfile);
});

app.MapDelete("/profile/{profileId}", async (IProfileService profileService, Guid profileId, CancellationToken cancellationToken) =>
{
    var deletedProfile = await profileService.DeleteProfileAsync(profileId, cancellationToken);

    return deletedProfile == null ? Results.NotFound() : Results.Ok(deletedProfile);
});

app.MapGet("/profile/GetLoggedInUserProfile", async (IProfileService profileService, HttpContext httpContext, CancellationToken cancellationToken) =>
{
    var userName = httpContext.User.Identity?.Name;
    if (string.IsNullOrEmpty(userName))
    {
        return Results.Unauthorized();
    }

    var profile = await profileService.GetProfileByUserNameAsync(userName, cancellationToken);

    return profile == null ? Results.NotFound() : Results.Ok(profile);
});

app.UseServiceDiscovery();

app.Run();
