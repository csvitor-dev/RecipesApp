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
        if (context.Exception is ProjectException projectException)
            HandleProjectException(context, projectException);
        else
            ThrowUnknownException(context);
    }

    private static void HandleProjectException(ExceptionContext context, ProjectException exception)
    {
        context.HttpContext
            .Response.StatusCode = (int)exception.StatusCode;
        context.Result = new ObjectResult(
            new ErrorListResponseJSON(exception.Payload)
        );
    }

    private static void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext
            .Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(
            new ErrorListResponseJSON(ResourcesAccessor.UNKNOWN_ERROR)
        );
    }
}