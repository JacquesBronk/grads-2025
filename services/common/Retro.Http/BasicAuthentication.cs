using System.Text;

namespace Retro.Http;

public class BasicAuthentication(string username, string password) : IAuthenticationStrategy
{
    public void ApplyAuthentication(HttpRequestMessage request)
    {
        var credentials = $"{username}:{password}";
        var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
    }
}