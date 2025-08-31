using System.Net;
using RecipesApp.Exception.Base;
using RecipesApp.Exception.Resources;

namespace RecipesApp.Exception.Project;

public class InvalidLoginException : ProjectException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public override IList<string> Payload => [ResourcesAccessor.EMAIL_OR_PASSWORD_INVALID];
}