using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Commands.Roles.Management.DeleteRole;
using TechOnIt.Application.Commands.Roles.Management.UpdateRole;
using TechOnIt.Application.Queries.Roles.GetAllRoles;

namespace TechOnIt.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage")]
[ApiController]
[Route("v1/[controller]/[action]")]
public class RoleController : ControllerBase
{
    #region DI & Ctor's
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
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
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete, Route("{id}")]
    [ApiResultFilter]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRoleCommand { Id = id }, cancellationToken);
        return Ok(result);
    }
    #endregion
}