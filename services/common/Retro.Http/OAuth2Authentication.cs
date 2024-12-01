namespace Retro.Http;

public class OAuth2Authentication(string accessToken) : IAuthenticationStrategy
{
    private readonly string _accessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));

    public void ApplyAuthentication(HttpRequestMessage request)
    {
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);
    }
}
