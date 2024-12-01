namespace Retro.Consul.HealthCheck;

public class ConsulHealthResponse
{
    public NodeInfo Node { get; set; }
    public ServiceInfo Service { get; set; }
    public List<CheckInfo> Checks { get; set; }
}

public class NodeInfo
{
    public string Node { get; set; }
    public string Address { get; set; }
}

public class ServiceInfo
{
    public string ID { get; set; }
    public string Service { get; set; }
    public string Address { get; set; }
    public int Port { get; set; }
}

public class CheckInfo
{
    public string CheckID { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public string Output { get; set; }
}