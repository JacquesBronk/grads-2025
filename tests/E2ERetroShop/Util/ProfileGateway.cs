using System.Net;
using System.Text;
using System.Text.Json;
using Retro.Http;
using Retro.Profile;

namespace E2ERetroShop.Util;

public class ProfileGateway
{
    private readonly string _baseUrl = "http://localhost:5000/profile-api/profile";
    private readonly AuthHelper _authHelper = new();
    
    public async Task<ProfileResponse?> GetProfileByIdAsync(Guid profileId, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        
        var stockItemUrl = $"{_baseUrl}?profileId={profileId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<ProfileResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<ProfileResponse?> CreateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        
        var stockItemUrl = $"{_baseUrl}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .WithContent(new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json"))
            .EnsureStatusCode(HttpStatusCode.Created)
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<ProfileResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<ProfileResponse?> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        
        var stockItemUrl = $"{_baseUrl}?profileId={profile.Id}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Put)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .WithContent(new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json"))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<ProfileResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<ProfileResponse> DeleteProfileAsync(Guid id, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        
        var stockItemUrl = $"{_baseUrl}?profileId={id}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Delete)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<ProfileResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<User?> GetKeyCloakUser(CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        
        var stockItemUrl = $"{_baseUrl}/GetKeyCloakUser";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<User>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
}