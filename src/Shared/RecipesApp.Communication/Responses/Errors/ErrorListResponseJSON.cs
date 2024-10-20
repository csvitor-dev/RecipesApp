namespace RecipesApp.Communication.Responses.Errors;

public record ErrorListResponseJSON
{
    public IList<string> Errors { get; } = null!;

    public ErrorListResponseJSON(IList<string> errors)
        => Errors = errors;
    public ErrorListResponseJSON(params string[] messages)
        : this(new List<string>())
    {
        foreach (string error in messages)
            Errors.Add(error);
    }
}
