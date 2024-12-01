using Yarp.ReverseProxy.Configuration;

namespace Retro.Yarp;

public static class ReverseProxyConfigExtensions
{
    public static IReverseProxyBuilder AddConfigBuilder(this IReverseProxyBuilder builder)
    {
        builder.Services.AddSingleton<IProxyConfigProvider, DynamicProxyConfigProvider>();
        return builder;
    }
}