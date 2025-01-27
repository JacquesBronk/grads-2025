using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Text.Json.Serialization;
using Retro.Http;
using Retro.Profile;

namespace E2ERetroShop.Util;

public class AuthHelper
{
    private const string authUrl = "http://localhost:8080/realms/retro-realm/protocol/openid-connect/token";
    private const string userName = "customerUser";
    private const string password = "Password1!";
    private const string client_secret = "k6LE3kUdj18kMa6eewhBWHLJTSeBPF2r";
    private const string client_id = "retro-client";
    private const string grant_type = "password";

    public async Task<string> GetTokenAsync()
    {
        var tokenResponse = await new RequestBuilder()
            .For(new Uri(authUrl))
            .WithMethod(HttpMethod.Post)
            .WithContent(new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["username"] = userName,
                ["password"] = password,
                ["client_id"] = client_id,
                ["client_secret"] = client_secret,
                ["grant_type"] = grant_type
            }))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(CancellationToken.None);

        var response = JsonSerializer.Deserialize<TokenResponse>(tokenResponse);
        
        return response?.AccessToken ?? string.Empty;
    }
    
    public User GetUserFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        
        return new User
        {
            UserName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value ?? string.Empty,
            UserId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? string.Empty
        };
    }
    
    private class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}