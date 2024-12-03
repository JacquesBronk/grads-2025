namespace Retro.ServiceDiscovery;

public class ServiceDetails
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
    public List<string> Tags { get; set; }
    public ServiceCheck Check { get; set; }
}