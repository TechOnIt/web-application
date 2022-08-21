using iot.Application.Commands.Roles.Management.CreateRole;
using iot.Application.Commands.Roles.Management.DeleteRole;
using iot.Application.Commands.Roles.Management.UpdateRole;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage"), Route("v1/[area]/[controller]")]
public class RoleController : BaseController
{
    #region DI & Ctor's
    public RoleController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        => await RunCommandAsync(command);

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
        => await RunCommandAsync(command);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(DeleteRoleCommand command)
        => await RunCommandAsync(command);
    #endregion

    #region Queries

    #endregion
}