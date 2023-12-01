using TechOnIt.Application.Queries.Users.Dashboard.GetNewUsersCount;

namespace TechOnIt.Admin.Web.Controllers;

public class DashboardController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNewUsersCountCommand(), cancellationToken);
        return View(result);
    }
}