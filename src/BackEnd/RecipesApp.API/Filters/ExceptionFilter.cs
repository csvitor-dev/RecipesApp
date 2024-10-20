using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipesApp.Communication.Responses.Errors;
using RecipesApp.Exception.Base;
using RecipesApp.Exception.Project;
using RecipesApp.Exception.Resources;

namespace RecipesApp.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ProjectException)
            HandleProjectException(context);
       else
            ThrowUnknowException(context);

    }
    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            ErrorOnValidationException ex = (context.Exception as ErrorOnValidationException)!;

            context.HttpContext
                .Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(new ErrorListResponseJSON(ex.ErrorMessages));
        }
    }
    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext
                .Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(
                new ErrorListResponseJSON(ResourcesAccessor.UNKNOWN_ERROR)
            );
    }
}
