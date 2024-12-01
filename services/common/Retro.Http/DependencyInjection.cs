using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Retro.Http;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddHttpClient(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        return builder;
    }
}