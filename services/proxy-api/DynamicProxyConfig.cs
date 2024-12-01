using Microsoft.Extensions.Primitives;
using Retro.Yarp.Domain;
using Yarp.ReverseProxy.Configuration;

namespace Retro.Yarp;

public class DynamicProxyConfig(ReverseProxyConfig config) : IProxyConfig
{
    public IReadOnlyList<RouteConfig> Routes => config.Routes;
    public IReadOnlyList<ClusterConfig> Clusters => config.Clusters;

    public IChangeToken ChangeToken => config.ChangeToken;
}