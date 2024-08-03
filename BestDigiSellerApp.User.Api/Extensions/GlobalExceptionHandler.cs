using BestDigiSellerApp.User.Entity.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;

namespace BestDigiSellerApp.User.Api.Extensions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {

                var statusCode = exception switch
                {
                    UserNotFoundException => StatusCodes.Status404NotFound,
                    ValidationException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status500InternalServerError
                };

                var exceptionDetail = new ProblemDetails
                {
                    Detail = exception.Message,
                    Status = statusCode,
                    Title = "An exception happened",
                    Type = exception.GetType().Name,
                };
                foreach (DictionaryEntry item in exception.Data)
                {
                    exceptionDetail.Extensions.Add(item.Key.ToString(), item.Value.ToString());
                }
                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(exceptionDetail);
                return true;
            }
            return false;
        }
    }
}
