namespace Retro.Http;

public interface IAuthenticationStrategy
{
    void ApplyAuthentication(HttpRequestMessage request);
}