using Microsoft.Extensions.Options;
using Retro.Yarp.Domain;
using Yarp.ReverseProxy.Configuration;

namespace Retro.Yarp;

public class DynamicProxyConfigProvider(IOptionsMonitor<ReverseProxyConfig> optionsMonitor) : IProxyConfigProvider
{
    public IProxyConfig GetConfig() => new DynamicProxyConfig(optionsMonitor.CurrentValue);
}