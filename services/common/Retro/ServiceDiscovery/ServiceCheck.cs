namespace Retro.ServiceDiscovery;

public class ServiceCheck
{
    public string HTTP { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }
}