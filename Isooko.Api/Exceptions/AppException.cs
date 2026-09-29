namespace Isooko.Api.Exceptions;

public abstract class AppException(string message, int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;

    public abstract string Type { get; }
}