namespace RecipesApp.Communication.Requests;

public record LoginUserRequestJSON(string Email = "", string Password = "");