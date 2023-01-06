using Microsoft.AspNetCore.Mvc;
using TechOnIt.Application.Commands.Structures.Authentication.SignInCommands;

namespace TechOnIt.Core.Api.Controllers.v1;

[Route("v1/[controller]/[action]")]
public class AuthController : ControllerBase
{
    #region DI & Ctor
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInStructureCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}