using System.Net;
using DCIAspNetTemplate.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DCIAspNetTemplate.Presentation;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers = new()
    {
      { typeof(BusinessLogicException), HandleBadRequestException },
      { typeof(ResourceNotFoundException), HandleNotFoundException }
    };

  public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex) when (_exceptionHandlers.TryGetValue(ex.GetType(), out var handler))
      {
        await handler(context, ex);
      }
      catch (Exception ex)
      {
        await HandleUnknownException(context, ex);
      }
    }

    private static Task HandleException(HttpContext context, String message, int code)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = code;

      var errorResponse = new
      {
        Message = message
      };

      return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }

    private static Task HandleBadRequestException(HttpContext context, Exception exception)
    {
      return HandleException(context, exception.Message, (int)HttpStatusCode.BadRequest);
    }

    private static Task HandleNotFoundException(HttpContext context, Exception exception)
    {
      return HandleException(context, exception.Message, (int)HttpStatusCode.NotFound);
    }

    private Task HandleUnknownException(HttpContext context, Exception exception)
    {
      _logger.LogError(exception, "An error occurred while processing the request.");

      return HandleException(
        context,
        "An unexpected error occurred.",
        (int)HttpStatusCode.InternalServerError
      );
    }
}