using System.Net;

namespace RecipesApp.Exception.Base;

public abstract class ProjectException : ApplicationException
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract IList<string> Payload { get; }
}