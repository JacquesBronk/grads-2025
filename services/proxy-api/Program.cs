using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Retro;
using Retro.Configuration;


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
    proxyPipeline.Use(async (context, next) =>
    {
        // If user is authenticated, set headers from claims
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst("sub")?.Value;
        
            // If that fails or you prefer reading raw token, do this:
            if (string.IsNullOrEmpty(userId))
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();
                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var rawJwt = authHeader["Bearer ".Length..];
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(rawJwt);

                    userId = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                context.Request.Headers["X-UserId"] = userId;
            }
  
            
            var username = context.User.FindFirst("preferred_username")?.Value;
            if (!string.IsNullOrEmpty(username))
            {
                context.Request.Headers["X-UserName"] = username;
            }
        }

        await next();
    });
});


app.UseServiceDiscovery();
app.Run();