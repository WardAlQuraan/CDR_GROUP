using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Infrastructure.Exceptions;

namespace cdr_group.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";
            List<string>? errors = null;

            switch (exception)
            {
                case ApiException apiException:
                    statusCode = apiException.StatusCode;
                    message = apiException.Message;
                    errors = apiException.Errors;
                    _logger.LogWarning(exception, "API Exception: {Message}", message);
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Unauthorized: {Message}", message);
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Invalid Operation: {Message}", message);
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Argument Exception: {Message}", message);
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Not Found: {Message}", message);
                    break;

                default:
                    _logger.LogError(exception, "Unhandled Exception: {Message}", exception.Message);
                    if (_environment.IsDevelopment())
                    {
                        message = exception.Message;
                        errors = new List<string> { exception.StackTrace ?? string.Empty };
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse.FailureResponse(message, errors);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
