using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Infrastructure.Exceptions;
using cdr_group.Domain.Localization;

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
            var language = GetLanguage(context);
            var statusCode = HttpStatusCode.InternalServerError;
            var messageKey = Messages.UnexpectedError;
            List<string>? errors = null;

            switch (exception)
            {
                case ApiException apiException:
                    statusCode = apiException.StatusCode;
                    messageKey = apiException.MessageKey;
                    errors = apiException.Errors;
                    _logger.LogWarning(exception, "API Exception: {MessageKey}", messageKey);
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    messageKey = Messages.Unauthorized;
                    _logger.LogWarning(exception, "Unauthorized: {Message}", exception.Message);
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    messageKey = exception.Message;
                    _logger.LogWarning(exception, "Invalid Operation: {Message}", exception.Message);
                    break;

                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    messageKey = exception.Message;
                    _logger.LogWarning(exception, "Argument Exception: {Message}", exception.Message);
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    messageKey = Messages.NotFound;
                    _logger.LogWarning(exception, "Not Found: {Message}", exception.Message);
                    break;

                default:
                    _logger.LogError(exception, "Unhandled Exception: {Message}", exception.Message);
                    if (_environment.IsDevelopment())
                    {
                        messageKey = exception.Message;
                        errors = new List<string> { exception.StackTrace ?? string.Empty };
                    }
                    break;
            }

            var message = Messages.Get(messageKey, language);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse.FailureResponse(message, errors);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }

        private static string GetLanguage(HttpContext context)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();

            if (string.IsNullOrEmpty(acceptLanguage))
                return "en";

            return acceptLanguage.StartsWith("ar", StringComparison.OrdinalIgnoreCase) ? "ar" : "en";
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
