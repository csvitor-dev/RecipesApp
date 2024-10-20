using System.Globalization;

namespace RecipesApp.API.Middlewares;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        string? requestedCulture = context.Request
            .Headers.AcceptLanguage.FirstOrDefault();
        requestedCulture = ValidStringCulture(requestedCulture);

        CultureInfo culture = new(requestedCulture);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
    private string ValidStringCulture(string? culture)
    {
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
        bool isValid = !string.IsNullOrWhiteSpace(culture) && cultures.Any((c) => c.Name.Equals(culture));

        return isValid ? culture! : "en";
    }
}
