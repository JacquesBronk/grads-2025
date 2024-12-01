namespace Retro.Http;

public class ApiKeyAuthentication(string apiKey, string headerName = "X-API-Key") : IAuthenticationStrategy
{
    private readonly string _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

    public void ApplyAuthentication(HttpRequestMessage request)
    {
        request.Headers.Add(headerName, _apiKey);
    }
}
