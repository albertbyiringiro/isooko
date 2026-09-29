using Isooko.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Isooko.Api.Common;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problem = exception switch
        {
            AppException appException => BuildProblem(appException, httpContext),
            _ => BuildUnexpectedProblem(httpContext)
        };

        if (exception is not AppException)
        {
            logger.LogError(exception, "Unhandled exception for {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }

        httpContext.Response.StatusCode = problem.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }

    private static ProblemDetails BuildProblem(AppException exception, HttpContext httpContext)
    {
        var problem = new ProblemDetails
        {
            Type = exception.Type,
            Title = exception.Message,
            Status = exception.StatusCode,
        };

        problem.Extensions["traceId"] = httpContext.TraceIdentifier;
        problem.Extensions["timestamp"] = DateTimeOffset.UtcNow;

        if (exception is ValidationException validationException)
        {
            problem.Extensions["errors"] = validationException.Errors;
        }

        return problem;
    }

    private static ProblemDetails BuildUnexpectedProblem(HttpContext httpContext)
    {
        var problem = new ProblemDetails
        {
            Type = "https://emporium.dev/errors/unexpected",
            Title = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
        };

        problem.Extensions["traceId"] = httpContext.TraceIdentifier;
        problem.Extensions["timestamp"] = DateTimeOffset.UtcNow;

        return problem;
    }
}