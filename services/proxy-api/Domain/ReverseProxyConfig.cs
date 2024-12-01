using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace Retro.Yarp.Domain;

public class ReverseProxyConfig
{
    /// <summary>
    /// Collection of route configurations.
    /// </summary>
    public IReadOnlyList<RouteConfig> Routes { get; set; } = new List<RouteConfig>();

    /// <summary>
    /// Collection of cluster configurations.
    /// </summary>
    public IReadOnlyList<ClusterConfig> Clusters { get; set; } = new List<ClusterConfig>();

    /// <summary>
    /// Token to track changes for dynamic updates.
    /// </summary>
    public IChangeToken ChangeToken { get; set; }
}