using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Middleware;

public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            // Just logging and returning 500 doesn't make sense
            // Let's customize the response and hide the stack trace
            logger.LogError(e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Internal server error",
                Title = "Internal server error",
                Detail = "An internal server has occurred, administrator has been notified"
            };

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);

            context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        }
    }
}