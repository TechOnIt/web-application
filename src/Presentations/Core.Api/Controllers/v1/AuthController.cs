﻿using Microsoft.AspNetCore.Mvc;
using TechOnIt.Application.Commands.Structures.Authentication.SignInCommands;

namespace TechOnIt.Board.Api.Controllers.v1;

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
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        // Wrong api key & password
        catch (IdentityArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        // Deactive structure.
        catch (StructureException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}