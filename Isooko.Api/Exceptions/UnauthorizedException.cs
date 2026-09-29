namespace Isooko.Api.Exceptions;

public class UnauthorizedException(string message) : AppException(message, StatusCodes.Status401Unauthorized)
{
    public override string Type => "https://isooko.dev/errors/unauthorized";
}