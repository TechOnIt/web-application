using TechOnIt.Application.Commands.Users.Management.CreateUser;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Users.Dashboard.GetUserInfoById;
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
    public async Task<IActionResult> Index([FromQuery] PaginatedSearch paginatedSearch, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(paginatedSearch.Keyword))
            ViewData["search"] = paginatedSearch.Keyword;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var createUserResult = await _mediator.Send(command, cancellationToken);
        return Redirect("/manage/user");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var userViewModel = await _mediator.Send(new FindUserViewModelByIdQuery()
        {
            Id = id
        }, cancellationToken);
        if (userViewModel == null)
        {
            // NotFound
        }
        return View(userViewModel);
    }
}