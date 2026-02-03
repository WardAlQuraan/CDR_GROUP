using Microsoft.AspNetCore.Http;

namespace cdr_group.Application.Helpers
{
    public static class UrlHelper
    {
        public static string? BuildFullUrl(string? relativePath, IHttpContextAccessor httpContextAccessor)
        {
            if (string.IsNullOrEmpty(relativePath))
                return null;

            var request = httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return relativePath;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/{relativePath.TrimStart('/')}";
        }
    }
}
