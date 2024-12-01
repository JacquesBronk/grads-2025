using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Retro.Http
{
    public class HttpRequestExecutor(
        Uri url,
        HttpMethod method,
        HttpContent? content,
        HttpStatusCode? expectedStatusCode,
        HttpStatusCode[]? expectedStatusCodes,
        Func<HttpResponseMessage, Task>? onFailure,
        Dictionary<string, string>? headers,
        JsonSerializerOptions? jsonSerializerOptions,
        IAuthenticationStrategy? authStrategy,
        TimeSpan? timeout,
        ILogger? logger = null,
        IHttpClientFactory? httpClientFactory = null)
    {
        private readonly Uri _url = url ?? throw new ArgumentNullException(nameof(url));
        private readonly HttpMethod _method = method ?? throw new ArgumentNullException(nameof(method));
        private readonly Dictionary<string, string> _headers = headers ?? new Dictionary<string, string>();
        private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions ?? new JsonSerializerOptions();

        public async Task<T> GetResultsAsAsync<T>(CancellationToken cancellationToken = default)
        {
            using var client = CreateHttpClient();
            var request = CreateHttpRequestMessage();

            logger?.LogInformation("Sending request to {Url} with method {Method}", _url, _method);

            var response = await client.SendAsync(request, cancellationToken);

            logger?.LogInformation("Received response with status code {StatusCode}", response.StatusCode);

            if (!IsStatusCodeExpected(response.StatusCode))
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                logger?.LogError("Request failed with status code {StatusCode}: {Response}", response.StatusCode, errorContent);

                if (onFailure != null)
                {
                    await onFailure(response);
                }

                throw new HttpRequestExceptionWithContent(
                    $"Unexpected status code: {response.StatusCode}",
                    response.StatusCode,
                    errorContent);
            }

            var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            try
            {
                var result = await JsonSerializer.DeserializeAsync<T>(responseStream, _jsonSerializerOptions, cancellationToken)
                             ?? throw new InvalidOperationException("Deserialization returned null.");

                logger?.LogInformation("Successfully deserialized response to {Type}", typeof(T));

                return result;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Deserialization failed");
                throw;
            }
        }

        public async Task<string> GetStringResultAsync(CancellationToken cancellationToken = default)
        {
            using var client = CreateHttpClient();
            var request = CreateHttpRequestMessage();

            logger?.LogInformation("Sending request to {Url} with method {Method}", _url, _method);

            var response = await client.SendAsync(request, cancellationToken);

            logger?.LogInformation("Received response with status code {StatusCode}", response.StatusCode);

            if (!IsStatusCodeExpected(response.StatusCode))
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                logger?.LogError("Request failed with status code {StatusCode}: {Response}", response.StatusCode, errorContent);

                if (onFailure != null)
                {
                    await onFailure(response);
                }

                throw new HttpRequestExceptionWithContent(
                    $"Unexpected status code: {response.StatusCode}",
                    response.StatusCode,
                    errorContent);
            }

            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

            logger?.LogInformation("Successfully retrieved response content");

            return responseContent;
        }

        public async IAsyncEnumerable<T> GetResultsAsAsyncEnumerable<T>(
            [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            using var client = CreateHttpClient();
            var request = CreateHttpRequestMessage();

            logger?.LogInformation("Sending request to {Url} with method {Method}", _url, _method);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            logger?.LogInformation("Received response with status code {StatusCode}", response.StatusCode);

            if (!IsStatusCodeExpected(response.StatusCode))
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                logger?.LogError("Request failed with status code {StatusCode}: {Response}", response.StatusCode, errorContent);

                if (onFailure != null)
                {
                    await onFailure(response);
                }

                throw new HttpRequestExceptionWithContent(
                    $"Unexpected status code: {response.StatusCode}",
                    response.StatusCode,
                    errorContent);
            }

            var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

            await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<T>(responseStream, _jsonSerializerOptions, cancellationToken))
            {
                if (item == null)
                {
                    continue;
                }

                yield return item;
            }

            logger?.LogInformation("Successfully streamed response as {Type}", typeof(T));
        }

        // New method to get just the status code
        public async Task<HttpStatusCode> GetStatusCodeAsync(CancellationToken cancellationToken = default)
        {
            using var client = CreateHttpClient();
            var request = CreateHttpRequestMessage();

            logger?.LogInformation("Sending request to {Url} with method {Method}", _url, _method);

            var response = await client.SendAsync(request, cancellationToken);

            logger?.LogInformation("Received response with status code {StatusCode}", response.StatusCode);

            if (!IsStatusCodeExpected(response.StatusCode))
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);

                logger?.LogError("Request failed with status code {StatusCode}: {Response}", response.StatusCode, errorContent);

                if (onFailure != null)
                {
                    await onFailure(response);
                }

                throw new HttpRequestExceptionWithContent(
                    $"Unexpected status code: {response.StatusCode}",
                    response.StatusCode,
                    errorContent);
            }

            logger?.LogInformation("Status code {StatusCode} matches expected status code", response.StatusCode);

            return response.StatusCode;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient client = httpClientFactory != null ? httpClientFactory.CreateClient() : new HttpClient();

            if (timeout.HasValue)
            {
                client.Timeout = timeout.Value;
            }

            return client;
        }

        private HttpRequestMessage CreateHttpRequestMessage()
        {
            var request = new HttpRequestMessage(_method, _url)
            {
                Content = content
            };

            authStrategy?.ApplyAuthentication(request);

            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            return request;
        }
        
        private bool IsStatusCodeExpected(HttpStatusCode statusCode)
        {
            if (expectedStatusCode.HasValue && statusCode == expectedStatusCode.Value)
            {
                return true;
            }

            return expectedStatusCodes != null && expectedStatusCodes.Contains(statusCode);
        }
    }
}
