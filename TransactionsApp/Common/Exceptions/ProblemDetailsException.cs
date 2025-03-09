using Microsoft.AspNetCore.Mvc;

namespace TransactionsApp.Common.Exceptions;

public class ProblemDetailsException : Exception
{
    public ProblemDetails ProblemDetails { get; }

    public ProblemDetailsException(ProblemDetails problemDetails)
        : base(problemDetails.Detail)
    {
        ProblemDetails = problemDetails;
    }
}