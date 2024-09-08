using LunaTestTask.Application.User.Commands.CreateTaskCommand;
using LunaTestTask.Application.User.Commands.DeleteTaskCommand;
using LunaTestTask.Application.User.Queries.GetTaskByIdQuery;
using LunaTestTask.Application.User.Queries.GetTasksQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LunaTestTask.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ApiBaseController
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get([FromQuery]GetTasksQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetTaskByIdQuery(id);
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post(CreateTaskCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new RemoveTaskCommand(id);
        await Mediator.Send(command);
        return NoContent();
    }
}