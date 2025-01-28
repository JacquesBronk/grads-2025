using System.Net;
using System.Text;
using System.Text.Json;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Http;

namespace E2ERetroShop.Util;

public class AdsGateway
{
    private readonly string _baseUrl = "http://localhost:5000/ads-admin-api/ads";
    private readonly AuthHelper _authHelper = new();
    
    
    public async Task<PagedAdDetailResponse> GetAdsFromDate(DateTimeOffset date, int pageSize, int page, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}/from-date?fromDate={date:F}&pageSize={pageSize}&pageNumber={page}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<PagedAdDetailResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<AdDetailResponse> GetAdByIdAsync(Guid adId, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}/{adId}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<AdDetailResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task<PagedAdDetailResponse> GetNAmountOfAds(int amount, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}/n?amount={amount}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<PagedAdDetailResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task CreateAdAsync(UpsertAdRequest ad, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Post)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .WithContent(new StringContent(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json"))
            .EnsureStatusCode(HttpStatusCode.Created)
            .Build()
            .GetStringResultAsync(cancellationToken);
    }
    
    public async Task<PagedAdDetailResponse> GetAllAds(int page, int pageSize, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}?pageNumber={page}&size={pageSize}";
        var stockItemResponse = await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Get)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);

        return JsonSerializer.Deserialize<PagedAdDetailResponse>(stockItemResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true });
    }
    
    public async Task UpdateAdAsync(UpsertAdRequest ad, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}";
        await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Put)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .WithContent(new StringContent(JsonSerializer.Serialize(ad), Encoding.UTF8, "application/json"))
            .EnsureStatusCode()
            .Build()
            .GetStringResultAsync(cancellationToken);
    }
    
    public async Task DeleteAdAsync(Guid adId, CancellationToken cancellationToken)
    {
        var token = await _authHelper.GetTokenAsync();
        var stockItemUrl = $"{_baseUrl}/{adId}";
        await new RequestBuilder()
            .For(new Uri(stockItemUrl))
            .WithMethod(HttpMethod.Delete)
            .WithAuthentication(new BearerTokenAuthentication(token))
            .EnsureStatusCode(HttpStatusCode.NoContent)
            .Build()
            .GetStringResultAsync(cancellationToken);
    }
}