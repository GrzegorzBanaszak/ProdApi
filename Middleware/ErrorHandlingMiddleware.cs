using System;
using System.Net;
using ProdApi.DomainException;
using System.Text.Json;

namespace ProdApi.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception for {Path}", context.Request.Path);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = MapStatusCode(ex);


            var payload = new
            {
                error = GetErrorCode(ex),
                message = ex.Message,
                traceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(payload);
            await context.Response.WriteAsync(json);
        }
    }

    private static int MapStatusCode(Exception ex) => ex switch
    {
        NotFoundException => (int)HttpStatusCode.NotFound,
        ValidationException => (int)HttpStatusCode.BadRequest,
        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
        _ => (int)HttpStatusCode.InternalServerError
    };

    private static string GetErrorCode(Exception ex) => ex switch
    {
        NotFoundException => "not_found",
        ValidationException => "validation_error",
        UnauthorizedAccessException => "unauthorized",
        _ => "internal_error"
    };

}
