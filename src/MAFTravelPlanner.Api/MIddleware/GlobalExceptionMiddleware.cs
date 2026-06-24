using System.Text.Json;

namespace MAFTravelPlanner.Api.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception");

            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    title = "Internal Server Error",
                    detail = ex.ToString()
                });
        }
    }
}