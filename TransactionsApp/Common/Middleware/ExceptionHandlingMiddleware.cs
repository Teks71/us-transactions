using Microsoft.AspNetCore.Mvc;
using TransactionsApp.Common.Exceptions;

namespace TransactionsApp.Common.Middleware
{
    /// <summary>
    ///   Middleware for handling exceptions.
    ///   Try returning a response with problem details in case
    ///   of a ProblemDetailsException and default details for other exceptions 
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ProblemDetailsException problemDetailsException)
            {
                _logger.LogError("ProblemDetails: {ProblemDetails}", problemDetailsException.ProblemDetails);
                await Results.Problem(problemDetailsException.ProblemDetails)
                    .ExecuteAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred: {Message}. Path: {Path}", ex.Message,
                    context.Request.Path);
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6",
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred on the server.",
                    Instance = context.Request.Path
                };
                await Results.Problem(problemDetails)
                    .ExecuteAsync(context);
            }
        }
    }
}