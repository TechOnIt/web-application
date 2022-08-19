using FluentResults;
using iot.Application.Commands;
using iot.Application.Commands.Users.Management;
using iot.Application.Commands.Users.Management.BanUser;
using iot.Application.Commands.Users.Management.CreateUser;
using iot.Application.Commands.Users.Management.ForceDelete;
using iot.Application.Commands.Users.Management.RemoveUserAccount;
using iot.Application.Commands.Users.Management.SetUserPassword;
using iot.Application.Commands.Users.Management.UnBanUser;
using iot.Application.Commands.Users.Management.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("Manage"), Route("[area]/v1/[controller]/")]
public class UserController : BaseController
{
    #region DI & Ctor's
    public UserController(IMediator mediator)
        : base(mediator)
    {
    }

    #endregion

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    => await RunCommandAsync(command);

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        => await RunCommandAsync(command);


    [HttpPatch]
    public async Task<IActionResult> SetPassword([FromBody] SetUserPasswordCommand command)
        => await RunCommandAsync(command);

    [HttpPatch("{id}")]
    public async Task<IActionResult> Ban([FromRoute] string id)
        => await RunCommandAsync(new BanUserCommand() { Id = id });

    [HttpPatch("{id}")]
    public async Task<IActionResult> UnBan([FromRoute] string id)
        => await RunCommandAsync(new UnBanUserCommand() { Id = id });


    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAccount([FromRoute] string id)
        => await RunCommandAsync(new RemoveUserAccountCommand() { Id = id });

    [HttpDelete("{id}")]
    public async Task<IActionResult> ForceDelete([FromRoute] string id)
        => await RunCommandAsync(new ForceDeleteUserCommand() { Id = id });
    #endregion

    #region Queries

    #endregion
}