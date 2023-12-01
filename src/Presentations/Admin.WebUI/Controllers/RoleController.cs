using System.Threading;
using TechOnIt.Application.Commands.Roles.Management.CreateRole;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Roles.GetAllRoles;

namespace TechOnIt.Admin.Web.Controllers;

public class RoleController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] PaginatedSearch paginate, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRolesQuery
        {
            Keyword = paginate.Keyword,
            Page = paginate.Page,
            PageSize = 20
        }, cancellationToken);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var createRoleResult = await _mediator.Send(command, cancellationToken);
        return Redirect("/manage/role");
    }
}