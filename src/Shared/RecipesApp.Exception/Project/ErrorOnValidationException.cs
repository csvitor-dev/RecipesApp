using System.Net;
using RecipesApp.Exception.Base;

namespace RecipesApp.Exception.Project;

public class ErrorOnValidationException(IList<string> errorMessages) : ProjectException
{
    public IList<string> ErrorMessages { get; } = errorMessages;

    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public override IList<string> Payload => ErrorMessages;
}