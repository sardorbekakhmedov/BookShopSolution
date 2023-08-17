namespace BookShop.Api.MiddleWares;

public class ErrorHandlerMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleWare> _logger;

    public ErrorHandlerMiddleWare(RequestDelegate next, ILogger<ErrorHandlerMiddleWare> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, "Internal server error BookShop!");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Error = e.Message });
        }
    }
}

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleWare(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleWare>();
    }
}