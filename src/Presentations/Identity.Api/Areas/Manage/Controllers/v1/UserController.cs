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

[Area("manage"), Route("v1/[area]/[controller]")]
public class UserController : BaseController
{
    #region DI & Ctor's
    public UserController(IMediator mediator)
        : base(mediator)
    {
    }

    #endregion

    #region Commands
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    => await RunCommandAsync(command);

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        => await RunCommandAsync(command);


    [HttpPatch("set-password")]
    public async Task<IActionResult> SetPassword([FromBody] SetUserPasswordCommand command)
        => await RunCommandAsync(command);

    [HttpPatch("ban/{id}")]
    public async Task<IActionResult> Ban([FromRoute] string id)
        => await RunCommandAsync(new BanUserCommand() { Id = id });

    [HttpPatch("unban/{id}")]
    public async Task<IActionResult> UnBan([FromRoute] string id)
        => await RunCommandAsync(new UnBanUserCommand() { Id = id });


    [HttpDelete("remove-account/{id}")]
    public async Task<IActionResult> RemoveAccount([FromRoute] string id)
        => await RunCommandAsync(new RemoveUserAccountCommand() { Id = id });

    [HttpDelete("force-delete/{id}")]
    public async Task<IActionResult> ForceDelete([FromRoute] string id)
        => await RunCommandAsync(new ForceDeleteUserCommand() { Id = id });
    #endregion

    #region Queries

    #endregion
}