using Microsoft.AspNetCore.Diagnostics;
using Project_Planner_API.Exceptions;

namespace Project_Planner_API
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails = new();

            if (exception is CustomException customException)
            {
                problemDetails.Code = customException.Code;
                problemDetails.Details = customException.Details;
            }
            else
            {
                problemDetails.Code = 500;
                problemDetails.Details = "Internal server error";
            }

            httpContext.Response.StatusCode = problemDetails.Code;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
