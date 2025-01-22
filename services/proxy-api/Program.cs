using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Retro;
using Retro.Configuration;
using Retro.Yarp.Infra;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfig("yarp");

builder.AddServiceDiscovery();
builder.Services.AddAuthorization(s => 
    s.AddPolicy("auth-policy", policyBuilder => policyBuilder.RequireAuthenticatedUser()));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    })
    .AddJwtBearer("Bearer", options =>
    {
        // For the incoming bearer token
        options.Authority = "http://auth:8080/realms/retro-realm";
        options.Audience = "retro-client";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            NameClaimType = "preferred_username",
            RoleClaimType = "roles",
            ValidateIssuer = false,
            ValidateActor = false,
          
            
        };
    })
    .AddOpenIdConnect(options =>
    {
        options.Authority = "http://auth:8080/realms/retro-realm";
        options.ClientId = "retro-client";
        options.ClientSecret = "k6LE3kUdj18kMa6eewhBWHLJTSeBPF2r";
        options.ResponseType = "code";
        options.SaveTokens = true;
        options.Scope.Add("openid");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "preferred_username",
            RoleClaimType = "roles"
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy(proxyPipeline =>
{
    // inject pipeline middleware for authentication

});


app.UseServiceDiscovery();
app.Run();