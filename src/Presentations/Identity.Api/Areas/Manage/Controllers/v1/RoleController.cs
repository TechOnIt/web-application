using iot.Application.Commands.Roles.Management.CreateRole;
using iot.Application.Commands.Roles.Management.DeleteRole;
using iot.Application.Commands.Roles.Management.UpdateRole;
using Microsoft.AspNetCore.DataProtection;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage")]
[Route("v1/[controller]/[action]")]
public class RoleController : ControllerBase
{
    #region DI & Ctor's
    private readonly IMediator _mediator;
    private readonly IDataProtector _dataProtectionProvider; // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionprovider?view=aspnetcore-6.0

    public RoleController(IMediator mediator, IDataProtectionProvider dataProtectionProvider)
    {
        _mediator = mediator;
        _dataProtectionProvider = dataProtectionProvider.CreateProtector("RouteData");
    }
    #endregion

    #region Command
    [HttpPost]
    [ApiResultFilter]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [ApiResultFilter]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [ApiResultFilter]
    [HttpDelete, Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!User.Identity.IsAuthenticated)
            return Unauthorized();

        var result = await _mediator.Send(new DeleteRoleCommand { Id = id });
        return Ok(result);
    }
    #endregion

    #region Queries
    //[ApiResultFilter]
    //[HttpGet("{roleName}")]
    //public async Task<IActionResult> GetRoleIdByRoleName(string roleName)
    //{

    //}
    #endregion
}