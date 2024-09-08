using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LunaTestTask.Controllers;

public class ApiBaseController : ControllerBase
{
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();
}
