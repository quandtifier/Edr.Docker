using System.Net;

namespace Edr.Api.Middleware;
public class CustomeExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomeExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<CustomeExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleGlobalExceptionsAsync(context, ex);
        }
    }

    private Task HandleGlobalExceptionsAsync(HttpContext context, Exception exception)
    {
        if (exception is ApplicationException)
        {
            _logger.LogWarning("Validation error occured in API. {message}", exception.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(new { exception.Message });
        }
        else
        {
            var errorId = Guid.NewGuid();
            _logger.LogError(exception, "Error occured in API: {ErrorId}", errorId);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsJsonAsync(new
            {
                ErrorId = errorId,
                Message = "Something bad happened in our API. " +
                "Contact QUANDTUM LLC support if problem persists."
            });
        }
    }
}

