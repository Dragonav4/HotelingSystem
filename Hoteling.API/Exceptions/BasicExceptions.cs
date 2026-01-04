using System.Net;
using Microsoft.AspNetCore.Identity;

namespace Hoteling.API.Exceptions;

public class BasicExceptions(
    string message,
    string errorCode,
    HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    : Exception(message)
{
    public string ErrorCode { get; } = errorCode;
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class UserNotFoundException(string identifier)
    : BasicExceptions($"User with identifier '{identifier}' was not found.", "USER_NOT_FOUND", HttpStatusCode.NotFound);

public class InvalidPasswordException()
    : BasicExceptions("Invalid email or password.", "INVALID_CREDENTIALS", HttpStatusCode.Unauthorized);

public class EmailAlreadyExistsException(string email)
    : BasicExceptions($"Email '{email}' is already taken.", "EMAIL_EXISTS", HttpStatusCode.Conflict);

public class RefreshTokenInvalidException()
    : BasicExceptions("Refresh token is invalid or expired.", "INVALID_REFRESH_TOKEN", HttpStatusCode.Unauthorized);

public class InvalidCredentialsException()
    : BasicExceptions("Invalid email or password.", "INVALID_CREDENTIALS", HttpStatusCode.Unauthorized);

public class IdentityValidationException(IEnumerable<IdentityError> errors)
    : BasicExceptions(
        "Validation failed: " + string.Join("; ", errors.Select(e => e.Description)),
        "IDENTITY_VALIDATION_ERROR",
        HttpStatusCode.BadRequest);

public class RoleAssignmentException(IEnumerable<IdentityError> errors)
    : BasicExceptions(
        "Failed to assign role: " + string.Join("; ", errors.Select(e => e.Description)),
        "ROLE_ASSIGNMENT_ERROR",
        HttpStatusCode.InternalServerError);

public class NotFoundException(string message, string errorCode = "NOT_FOUND")
    : BasicExceptions(message, errorCode, HttpStatusCode.NotFound);

public class ConflictException(string message, string errorCode = "CONFLICT")
    : BasicExceptions(message, errorCode, HttpStatusCode.Conflict);

public class ForbiddenException(string message = "You are not allowed to access this resource", string errorCode = "FORBIDDEN")
    : BasicExceptions(message, errorCode, HttpStatusCode.Forbidden);
