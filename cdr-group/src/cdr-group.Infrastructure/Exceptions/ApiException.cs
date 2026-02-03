using System.Net;

namespace cdr_group.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public List<string>? Errors { get; }

        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, List<string>? errors = null)
            : base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string entityName, Guid id)
            : base($"{entityName} with ID '{id}' was not found.", HttpStatusCode.NotFound)
        {
        }
    }

    public class BadRequestException : ApiException
    {
        public BadRequestException(string message, List<string>? errors = null)
            : base(message, HttpStatusCode.BadRequest, errors)
        {
        }
    }

    public class ConflictException : ApiException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }

    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message = "Unauthorized access.")
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }

    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message = "Access forbidden.")
            : base(message, HttpStatusCode.Forbidden)
        {
        }
    }

    public class ValidationException : ApiException
    {
        public ValidationException(List<string> errors)
            : base("One or more validation errors occurred.", HttpStatusCode.BadRequest, errors)
        {
        }

        public ValidationException(string error)
            : base("Validation error occurred.", HttpStatusCode.BadRequest, new List<string> { error })
        {
        }
    }
}
