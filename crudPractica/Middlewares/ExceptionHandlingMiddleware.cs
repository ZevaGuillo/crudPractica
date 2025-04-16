using Microsoft.EntityFrameworkCore;

namespace crudPractica.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "ArgumentException atrapada");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(ex, "DbUpdateException atrapada");
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception atrapada");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { message = ex.Message });
            }
        }
    }
}
