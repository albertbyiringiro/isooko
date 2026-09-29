namespace Isooko.Api.Exceptions;

public class NotFoundException(string message) : AppException(message, StatusCodes.Status404NotFound)
{
    public override string Type => "https://isooko.dev/errors/not-found";
}