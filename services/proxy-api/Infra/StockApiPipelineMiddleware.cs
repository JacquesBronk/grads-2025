using Microsoft.AspNetCore.Authentication;

namespace Retro.Yarp.Infra;

public class StockApiPipelineMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/stock-api"))
        {
            var path = context.Request.Path.Value?.ToLower();
            var method = context.Request.Method;

            // Allow anonymous for specific routes
            if (method == HttpMethods.Get &&
                (path == "/stock-api" || 
                 path.StartsWith("/stock-api/search") || 
                 path.StartsWith("/stock-api/{id}") ||
                 path.StartsWith("/stock-api/health")))
            {
                await _next(context);
                return;
            }

            // Enforce authentication
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await context.ChallengeAsync();
                return;
            }
        }

        await _next(context);
    }
}