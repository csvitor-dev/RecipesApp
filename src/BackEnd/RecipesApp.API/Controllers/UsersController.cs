using Microsoft.AspNetCore.Mvc;
using RecipesApp.Application.UseCases.User.Register;
using RecipesApp.Communication.Requests;
using RecipesApp.Communication.Responses;

namespace RecipesApp.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserResponseJSON), StatusCodes.Status201Created)]
    public IActionResult Register(RegisterUserRequestJSON request)
    {
        RegisterUserUC uc = new();
        RegisterUserResponseJSON result = uc.Execute(request);
        
        return Created(string.Empty, result);
    }
}
