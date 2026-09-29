namespace Isooko.Api.Exceptions;


public class ConflictException(string message) : AppException(message, StatusCodes.Status409Conflict)
{
    public override string Type => "https://emporium.dev/errors/conflict";
}