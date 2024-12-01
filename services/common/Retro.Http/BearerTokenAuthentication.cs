namespace Retro.Http;

public class BearerTokenAuthentication(string token) : IAuthenticationStrategy
{
    public void ApplyAuthentication(HttpRequestMessage request)
    {
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
}