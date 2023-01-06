using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Commands.Roles.Management.DeleteRole;
using TechOnIt.Application.Commands.Roles.Management.UpdateRole;
using TechOnIt.Application.Queries.Roles.GetAllRoles;

namespace TechOnIt.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage")]
<<<<<<< HEAD
[ApiController]
[Route("v1/[controller]/[action]")]
public class RoleController : ControllerBase
=======
[Authorize]
public class RoleController : BaseController
>>>>>>> b5fee11ae1e31c4c057823a79711086a6a51ad1a
{
    #region DI & Ctor's
    private readonly IMediator _mediator;
    //private readonly IDataProtector _dataProtectionProvider; // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.dataprotection.idataprotectionprovider?view=aspnetcore-6.0

    public RoleController(IMediator mediator/*, IDataProtectionProvider dataProtectionProvider*/)
    {
        _mediator = mediator;
        //_dataProtectionProvider = dataProtectionProvider.CreateProtector("RouteData");
    }
    #endregion

    #region Queries
    [HttpGet]
    [ApiResultFilter]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRolesQuery(), cancellationToken);
        return Ok(result);
    }

    #endregion

    #region Command
    [HttpPost]
    [ApiResultFilter]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ApiResultFilter]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command,cancellationToken);
        return Ok(result);
    }

    [HttpDelete, Route("{id}")]
    [ApiResultFilter]
    public async Task<IActionResult> Delete(string id,CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRoleCommand { Id = id }, cancellationToken);
        return Ok(result);
    }
    #endregion
}