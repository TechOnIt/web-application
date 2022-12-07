using TechOnIt.Application.Commands.Structures.Authentication.SignInCommands;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;

namespace TechOnIt.Core.Api.Controllers.v1;

[Route("[controller]/[action]")]
public class AuthController : BaseController
{
    #region DI & Ctor
    public AuthController(IMediator _mediator)
        : base(_mediator)
    { }
    #endregion

    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInStructureCommand command)
        => await RunCommandAsync(command);
}