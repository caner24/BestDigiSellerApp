﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BestDigiSellerApp.Stripe.Api.Extensions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {
                var statusCode = exception switch
                {
                    _ => StatusCodes.Status500InternalServerError
                };

                var exceptionDetail = new ProblemDetails
                {
                    Detail = exception.Message,
                    Status = statusCode,
                    Title = "An exception happened",
                    Type = exception.GetType().Name,
                };

                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(exceptionDetail);
                return true;
            }
            return false;
        }
    }
}
