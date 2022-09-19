using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Common.Exceptions;

namespace Shop.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BadRequest)
        {
            context.Result = new BadRequestObjectResult(new ErrorResponse(StatusCodes.Status400BadRequest, context.Exception.Message));
        }
    }
}

public record ErrorResponse(int StatusCode, string Message);
