using FluentValidation;
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
            context.Result = new BadRequestObjectResult(new ErrorResponse(StatusCodes.Status400BadRequest, new List<string>{context.Exception.Message}));
        }

        if (context.Exception is ValidationException)
        {
            var ex = (ValidationException)context.Exception;
            var messages = ex.Errors.Select(v => v.ErrorMessage).ToList();
            context.Result = new BadRequestObjectResult(new ErrorResponse(StatusCodes.Status400BadRequest, messages));
        }
    }
}

public record ErrorResponse(int StatusCode, IList<string> Messages);
