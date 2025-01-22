using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Retro.Handlers;

/// <summary>
/// Global exception handler for handling exceptions and returning ProblemDetails.
/// </summary>
internal class ExceptionHandler(
    ILogger<ExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        return await HandleExceptionAsync(httpContext, exception);
    }

    private async ValueTask<bool> HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        logger.LogError(exception, "Exception: {Message}", exception.Message);

        var statusCode = GetStatusCode(exception);
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Detail = GetErrors(exception),
            Title = GetTitle(exception),
            Type = GetType(exception),
            Instance = GetRequestInstance(httpContext),
            Extensions = GetExtensions(exception, httpContext)
        };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        });
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            InvalidOperationException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(Exception exception)
    {
        return exception switch
        {
            ValidationException => "Validation Error",
            ArgumentNullException => "Missing Required Argument",
            UnauthorizedAccessException => "Unauthorized Access",
            NotImplementedException => "Feature Not Implemented",
            InvalidOperationException => "Invalid Operation",
            _ => "An Unexpected Error Occurred"
        };
    }

    private static string GetType(Exception exception)
    {
        return exception switch
        {
            ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ArgumentNullException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            UnauthorizedAccessException => "https://www.rfc-editor.org/rfc/rfc7235#section-3.1",
            NotImplementedException => "https://tools.ietf.org/html/rfc7231#section-6.6.2",
            InvalidOperationException => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
            _ => "https://www.rfc-editor.org/rfc/rfc7231"
        };
    }

    private static string GetErrors(Exception exception)
    {
        var errorMessages = new List<string>();

        if (exception is ValidationException validationException)
        {
            var validationErrors = validationException.Errors
                .ToLookup(x => x.PropertyName, x => x.ErrorMessage);

            errorMessages.AddRange(
                from @group
                    in validationErrors
                let property = @group.Key
                let messages = string.Join(", ", @group)
                select $"Property '{property}': {messages}");
        }

        if (!string.IsNullOrWhiteSpace(exception.Message))
        {
            errorMessages.Add(exception.Message);
        }
        
        if (exception.InnerException is not null)
        {
            errorMessages.Add($"Inner Exception: {exception.InnerException.Message}"); 
        }

        return errorMessages.Count > 0
            ? string.Join("; ", errorMessages)
            : "An unexpected error occurred.";
    }

    private static string GetRequestInstance(HttpContext httpContext)
    {
        var method = httpContext.Request.Method;

        var route = string.Join("/", httpContext.Request.RouteValues.Select(kv => $"{kv.Key}={kv.Value}"));

        return $"[{method}] - {httpContext.Request.PathBase}{httpContext.Request.Path} ({route})";
    }

    private static Dictionary<string, object?> GetExtensions(Exception exception, HttpContext httpContext)
    {
        return new Dictionary<string, object?>
        {
            ["ExceptionType"] = exception.GetType().Name,
            ["TraceId"] = httpContext.TraceIdentifier,
            ["Success"] = false
        };  
    }
}