namespace Retro.Yarp.Infra;

public class KeycloakAuth
{
    public string Authority { get; set; }
    public bool PublicClient { get; set; }
    public string ClientId { get; set; }
    public string SslRequired { get; set; }
    public int ConfidentialPort { get; set; }
    public bool VerifyTokenAudience { get; set; }
    public string ClientSecret { get; set; }
}