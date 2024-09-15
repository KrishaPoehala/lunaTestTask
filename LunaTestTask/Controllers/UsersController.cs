using LunaTestTask.Application.User.Commands.LoginUserCommand;
using LunaTestTask.Application.User.Commands.RegisterUserCommand;
using Microsoft.AspNetCore.Mvc;

namespace LunaTestTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiBaseController
{

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserCommand request)
    {
        var result = await Mediator.Send(request);
        return Ok(result);
    }
}
