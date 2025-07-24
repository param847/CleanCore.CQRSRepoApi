using Domain.Entities.Logging;
using Infrastructure.Persistence.Contexts;

namespace Presentation.Api.Middlewares
{
    /// <summary>
    /// Catches unhandled exceptions, logs them to the database, and returns a generic 500 response.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, AppDbContext dbContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // 1. Log to Console/Structured logger
                _logger.LogError(ex, "Unhandled exception caught by middleware");

                // 2. Save to DB
                try
                {
                    var log = new ExceptionLog(ex, additionalInfo: $"Path: {httpContext.Request.Path}");
                    dbContext.ExceptionLogs.Add(log);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception dbEx)
                {
                    // If DB logging fails, log to fallback
                    _logger.LogError(dbEx, "Failed to write exception log to DB");
                }

                // 3. Return generic 500 response
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Error = "An unexpected error occurred. Please try again later."
                });
            }
        }
    }
}
