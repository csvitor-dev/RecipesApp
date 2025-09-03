using Microsoft.AspNetCore.Mvc;
using RecipesApp.Application.UseCases.User.Login.DoLogin;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;
using RecipesApp.Communication.Responses.Errors;

namespace RecipesApp.API.Controllers;

public class AuthController : RecipesAppBaseController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(RegisterUserResponseJSON), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorListResponseJSON), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login
    (
        [FromServices] IDoLoginUC uc,
        [FromBody] LoginUserRequestJSON request
    )
    {
        var result = await uc.Execute(request);

        return Ok(result);
    }
}