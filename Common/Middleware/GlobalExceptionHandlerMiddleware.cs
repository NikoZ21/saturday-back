using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Saturday_Back.Common.Exceptions;
using Saturday_Back.Features.Schedules.Dtos;
using System.Net;
using System.Text.Json;

namespace Saturday_Back.Common.Middleware
{
    /// <summary>
    /// Global exception handler middleware that catches all unhandled exceptions
    /// and converts them to appropriate HTTP responses with proper status codes.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed in {Layer} layer: {Message}", ex.ValidationLayer, ex.Message);
                await HandleExceptionAsync(context, ex, ex.StatusCode, ex.GetType().Name);
            }
            catch (BusinessRuleException ex)
            {
                _logger.LogWarning(ex, "Business rule violation: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, ex.StatusCode, ex.GetType().Name, ex.UserMessage);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError(ex, "Data access error: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, ex.StatusCode, ex.GetType().Name);
            }
            catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx)
            {
                var (statusCode, message) = MySqlExceptionHandler.HandleMySqlException(mysqlEx);
                _logger.LogError(ex, "Database error: {Message} (MySQL Error {ErrorNumber})", message, mysqlEx.Number);
                await HandleExceptionAsync(context, ex, statusCode, "DatabaseException", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.InternalServerError, "InternalServerError");
            }
        }

        private static Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            int statusCode,
            string exceptionType,
            string? customMessage = null)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = statusCode,
                message = customMessage ?? exception.Message,
                type = exceptionType,
                timestamp = DateTime.UtcNow,
                data = new { scheduleEntries = Array.Empty<ScheduleEntryResponseDto>() }
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(json);
        }
    }
}

