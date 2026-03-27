using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Localization;

namespace cdr_group.Infrastructure.Middleware
{
    public class ActiveUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ActiveUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    var user = await userRepository.GetByIdAsync(userId);

                    if (user == null || !user.IsActive)
                    {
                        var language = context.Request.Headers["Accept-Language"].FirstOrDefault();
                        var lang = string.IsNullOrEmpty(language) ? "en"
                            : language.StartsWith("ar", StringComparison.OrdinalIgnoreCase) ? "ar" : "en";

                        var message = Messages.Get(Messages.AccountDeactivated, lang);

                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse.FailureResponse(message);

                        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });

                        await context.Response.WriteAsync(json);
                        return;
                    }
                }
            }

            await _next(context);
        }
    }

    public static class ActiveUserMiddlewareExtensions
    {
        public static IApplicationBuilder UseActiveUserCheck(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ActiveUserMiddleware>();
        }
    }
}
