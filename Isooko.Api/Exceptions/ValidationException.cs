namespace Isooko.Api.Exceptions;

public class ValidationException(IDictionary<string, string[]> errors) : AppException("One or more validation errors occured.", StatusCodes.Status400BadRequest)
{
    public override string Type => "https://isooko.dev/dev/errors/validation";

    public IDictionary<string, string[]> Errors { get; } = errors;
}