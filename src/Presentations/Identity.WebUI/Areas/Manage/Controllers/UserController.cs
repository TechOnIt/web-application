using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Users.GetAllUsers;

namespace TechOnIt.Identity.WebUI.Areas.Manage.Controllers;

[Area("Manage")]
public class UserController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> Index(PaginatedSearch paginatedSearch, CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersQuery
        {
            Page = paginatedSearch.Page,
            Keyword = paginatedSearch.Keyword
        }
        , cancellationToken);
        return View(users);
    }

    [HttpGet]
    public IActionResult Info()
    {
        return View();
    }
}