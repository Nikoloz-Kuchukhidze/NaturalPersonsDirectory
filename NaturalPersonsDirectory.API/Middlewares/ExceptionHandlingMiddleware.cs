using Microsoft.AspNetCore.Mvc;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using System.Text.Json;

namespace NaturalPersonsDirectory.API.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            BaseException baseException => (int)baseException.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails()
        {
            Status = statusCode,
            Title = exception.Message,
            Detail = $"InnerException Message: {exception.InnerException?.Message}",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["stackTrace"] = exception.StackTrace;

        string json = JsonSerializer.Serialize(problemDetails);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(json);
    }
}
