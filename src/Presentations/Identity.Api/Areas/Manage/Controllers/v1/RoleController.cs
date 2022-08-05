using iot.Application.Commands.Roles.Management;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("Manage"), Route("[area]/v1/[controller]/")]
public class RoleController : BaseController
{
    #region DI & Ctor's
    public RoleController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        => await RunCommandAsync(command);
    #endregion

    #region Queries

    #endregion
}