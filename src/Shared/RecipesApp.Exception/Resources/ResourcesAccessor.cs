namespace RecipesApp.Exception.Resources;

public static class ResourcesAccessor
{
    public static string EMAIL_INVALID { get 
            => ExceptionMessagesResource.EMAIL_INVALID; }
    public static string EMAIL_REQUIRED { get 
            => ExceptionMessagesResource.EMAIL_REQUIRED; }
    public static string NAME_REQUIRED { get 
            => ExceptionMessagesResource.NAME_REQUIRED; }
    public static string PASSWORD_LENGTH { get 
            => ExceptionMessagesResource.PASSWORD_LENGTH; }
    public static string PASSWORD_REQUIRED { get 
            => ExceptionMessagesResource.PASSWORD_REQUIRED; }
    public static string UNKNOWN_ERROR { get 
            => ExceptionMessagesResource.UNKNOWN_ERROR; }
    public static string SERVICE_FAIL { get 
            => ExceptionMessagesResource.SERVICE_FAIL; }
}
