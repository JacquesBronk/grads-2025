using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Handlers;
using Retro.ServiceDiscovery;

namespace Retro;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddServiceDiscovery(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ServiceDetails>(builder.Configuration.GetRequiredSection("ServiceDetails"));
        
        builder.Services.AddTransient<IServiceDiscoveryRegistrar, ServiceDiscoveryRegistrar>();
        return builder;
    }
    
    public static WebApplication UseServiceDiscovery(this WebApplication app)
    {
        var registrar = app.Services.GetRequiredService<IServiceDiscoveryRegistrar>();
        registrar.RegisterServiceAsync().GetAwaiter().GetResult();
        return app;
    }
    
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
    
    public static WebApplication UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler();
        return app;
    }
}