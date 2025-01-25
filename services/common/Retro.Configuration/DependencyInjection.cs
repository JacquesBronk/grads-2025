using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Winton.Extensions.Configuration.Consul;


namespace Retro.Configuration;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddConfig(this WebApplicationBuilder builder, string kvPath)
    {
        if (string.IsNullOrWhiteSpace(kvPath))
        {
            throw new ConsulConfigurationException("Key-Value path cannot be null or empty.");
        }
        
        //if environment is not Develop use "http://consul:8500" ELSE use "http://localhost:8500"
        var consulAddress = builder.Configuration["Environment"] == "Development" ? "http://localhost:8500" : "http://consul:8500";
        
        builder.Configuration
            .AddConsul($"app-settings/{kvPath}",
                options =>
                {
                    options.ConsulConfigurationOptions = cco =>
                    {
                        cco.Address = new Uri(consulAddress);
                    };
                    options.ReloadOnChange = true; 
                    options.Optional = true;
                    options.OnLoadException = exceptionContext => throw exceptionContext.Exception;
                });

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("AllowAll", p =>
            {
                p.AllowAnyOrigin();
                p.AllowAnyMethod();
                p.AllowAnyHeader();
            });
        });
        
        return builder;
    }
}