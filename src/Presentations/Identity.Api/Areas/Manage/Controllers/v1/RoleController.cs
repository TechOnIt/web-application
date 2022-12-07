using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Commands.Roles.Management.DeleteRole;
using TechOnIt.Application.Commands.Roles.Management.UpdateRole;
using TechOnIt.Application.Queries.Roles.GetAllRoles;
using Microsoft.AspNetCore.DataProtection;
using TechOnIt.Identity.Api.Controllers;

namespace TechOnIt.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage")]
public class RoleController : BaseController
{
    #region DI & Ctor's
    private readonly IMediator _mediator;
    private readonly IDataProtector _dataProtectionProvider; // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionprovider?view=aspnetcore-6.0

    public RoleController(IMediator mediator, IDataProtectionProvider dataProtectionProvider)
        : base(mediator)
    {
        _mediator = mediator;
        _dataProtectionProvider = dataProtectionProvider.CreateProtector("RouteData");
    }
    #endregion

    #region Queries
    [HttpGet]
    [ApiResultFilter]
    public async Task<IActionResult> GetAll([FromQuery] GetRolesQuery query, CancellationToken cancellationToken)
        => await ExecuteAsync(query, cancellationToken);
    #endregion

    #region Command
    [HttpPost]
    [ApiResultFilter]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
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
}