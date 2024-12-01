using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Retro.Http;

public class RequestBuilder
{
    private Uri? _url;
    private HttpMethod _method = HttpMethod.Get;
    private HttpContent? _content;
    private HttpStatusCode _expectedStatusCode = HttpStatusCode.OK;
    private HttpStatusCode[]? _expectedStatusCodes = null;
    private Func<HttpResponseMessage, Task>? _onFailure;
    private readonly Dictionary<string, string> _headers = new();
    private JsonSerializerOptions _jsonSerializerOptions = new();
    private TimeSpan? _timeout;
    private IAuthenticationStrategy? _authStrategy;
    private ILogger? _logger;
    private IHttpClientFactory? _httpClientFactory;


    public static RequestBuilder BuildRequest() => new RequestBuilder();

    public RequestBuilder For(Uri url)
    {
        _url = url ?? throw new ArgumentNullException(nameof(url));
        return this;
    }

    public RequestBuilder WithMethod(HttpMethod method)
    {
        _method = method ?? throw new ArgumentNullException(nameof(method));
        return this;
    }

    public RequestBuilder Get()
    {
        _method = HttpMethod.Get;
        return this;
    }

    public RequestBuilder Post()
    {
        _method = HttpMethod.Post;
        return this;
    }

    public RequestBuilder Post(HttpContent content)
    {
        _method = HttpMethod.Post;
        _content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }

    public RequestBuilder Post<T>(T content)
    {
        _method = HttpMethod.Post;
        return WithJsonContent(content);
    }

    public RequestBuilder Put()
    {
        _method = HttpMethod.Put;
        return this;
    }

    public RequestBuilder Put(HttpContent content)
    {
        _method = HttpMethod.Put;
        _content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }

    public RequestBuilder Put<T>(T content)
    {
        _method = HttpMethod.Put;
        return WithJsonContent(content);
    }

    public RequestBuilder Patch()
    {
        _method = HttpMethod.Patch;
        return this;
    }

    public RequestBuilder Patch(HttpContent content)
    {
        _method = HttpMethod.Patch;
        _content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }

    public RequestBuilder Patch<T>(T content)
    {
        _method = HttpMethod.Patch;
        return WithJsonContent(content);
    }

    public RequestBuilder Delete()
    {
        _method = HttpMethod.Delete;
        return this;
    }

    public RequestBuilder WithContent(HttpContent content)
    {
        _content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }

    public RequestBuilder WithJsonContent<T>(T content)
    {
        var jsonString = JsonSerializer.Serialize(content, _jsonSerializerOptions);
        _content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        return this;
    }

    public RequestBuilder WithXmlContent<T>(T content, Encoding? encoding = null)
    {
        var xmlString = SerializeToXml(content);
        encoding ??= Encoding.UTF8;
        _content = new StringContent(xmlString, encoding, "application/xml");
        return this;
    }

    private string SerializeToXml<T>(T content)
    {
        using var stringWriter = new System.IO.StringWriter();
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        serializer.Serialize(stringWriter, content);
        return stringWriter.ToString();
    }

    public RequestBuilder WithUrlFormEncodedContent(IEnumerable<KeyValuePair<string, string>> formData)
    {
        _content = new FormUrlEncodedContent(formData);
        return this;
    }

    public RequestBuilder WithHeaders(IDictionary<string, string> headers)
    {
        foreach (var header in headers)
        {
            _headers[header.Key] = header.Value;
        }

        return this;
    }

    public RequestBuilder WithHeaders(IEnumerable<KeyValuePair<string, string>> headers)
    {
        foreach (var header in headers)
        {
            _headers[header.Key] = header.Value;
        }

        return this;
    }

    public RequestBuilder WithHeader(string key, string value)
    {
        _headers[key] = value;
        return this;
    }

    public RequestBuilder WithJsonSettings(JsonSerializerOptions options)
    {
        _jsonSerializerOptions = options ?? throw new ArgumentNullException(nameof(options));
        return this;
    }

    public RequestBuilder WithTimeout(TimeSpan timeout)
    {
        _timeout = timeout;
        return this;
    }

    public RequestBuilder EnsureStatusCode(HttpStatusCode code = HttpStatusCode.OK)
    {
        _expectedStatusCode = code;
        return this;
    }
    
    public RequestBuilder EnsureStatusCode(HttpStatusCode[] codes)
    {
        _expectedStatusCodes = codes;
        return this;
    }

    public RequestBuilder WhenFailed(Func<HttpResponseMessage, Task>? onFailure)
    {
        _onFailure = onFailure;
        return this;
    }

    public RequestBuilder WithAuthentication(IAuthenticationStrategy authStrategy)
    {
        _authStrategy = authStrategy ?? throw new ArgumentNullException(nameof(authStrategy));
        return this;
    }

    public RequestBuilder WithLogger(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        return this;
    }

    public RequestBuilder WithHttpClientFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        return this;
    }

    public HttpRequestExecutor Build()
    {
        return new HttpRequestExecutor(
            _url,
            _method,
            _content,
            _expectedStatusCode,
            _expectedStatusCodes,
            _onFailure,
            _headers,
            _jsonSerializerOptions,
            _authStrategy,
            _timeout,
            _logger,
            _httpClientFactory);
    }
}