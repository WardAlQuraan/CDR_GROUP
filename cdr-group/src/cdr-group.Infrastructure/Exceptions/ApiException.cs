using System.Net;

namespace cdr_group.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public List<string>? Errors { get; }
        public string MessageKey { get; }

        public ApiException(string messageKey, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, List<string>? errors = null)
            : base(messageKey)
        {
            MessageKey = messageKey;
            StatusCode = statusCode;
            Errors = errors;
        }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string messageKey)
            : base(messageKey, HttpStatusCode.NotFound)
        {
        }
    }

    public class BadRequestException : ApiException
    {
        public BadRequestException(string messageKey, List<string>? errors = null)
            : base(messageKey, HttpStatusCode.BadRequest, errors)
        {
        }
    }

    public class ConflictException : ApiException
    {
        public ConflictException(string messageKey)
            : base(messageKey, HttpStatusCode.Conflict)
        {
        }
    }

    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string messageKey = "unauthorized")
            : base(messageKey, HttpStatusCode.Unauthorized)
        {
        }
    }

    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string messageKey = "forbidden")
            : base(messageKey, HttpStatusCode.Forbidden)
        {
        }
    }

    public class ValidationException : ApiException
    {
        public ValidationException(List<string> errors)
            : base("validation_error", HttpStatusCode.BadRequest, errors)
        {
        }

        public ValidationException(string error)
            : base("validation_error", HttpStatusCode.BadRequest, new List<string> { error })
        {
        }
    }
}
