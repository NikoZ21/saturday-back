using Serilog.Context;

namespace Saturday_Back.Common.Middleware
{
    /// <summary>
    /// Middleware that generates a correlation ID for each request and adds it to the log context.
    /// This allows tracking all logs related to a single request across Controller -> Service -> Repository.
    /// </summary>
    public class CorrelationIdMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private const string CorrelationIdPropertyName = "CorrelationId";

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to get correlation ID from request header, or generate a new one
            var correlationId = Guid.NewGuid().ToString();

            // Add it to Serilog's LogContext so ALL logs in this request will include it
            using (LogContext.PushProperty(CorrelationIdPropertyName, correlationId))
            {
                await _next(context);
            }
        }
    }
}