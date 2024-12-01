using System.Net;

namespace Retro.Http;

public class HttpRequestExceptionWithContent : HttpRequestException
{
    public HttpStatusCode StatusCode { get; }
    public string ResponseContent { get; }

    public HttpRequestExceptionWithContent(string message, HttpStatusCode statusCode, string responseContent)
        : base(message)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}