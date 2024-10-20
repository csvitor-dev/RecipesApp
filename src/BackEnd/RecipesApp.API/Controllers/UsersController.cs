using Microsoft.AspNetCore.Mvc;
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
        RegisterUserResponseJSON response = new(request.Name);
        
        return Created(string.Empty, response);
    }
}
