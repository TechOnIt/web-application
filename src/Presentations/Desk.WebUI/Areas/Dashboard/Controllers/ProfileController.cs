using TechOnIt.Application.Commands.Users.Dashboards.ProfileCommands;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class ProfileController : Controller
{
    private IMediator _mediator;
    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var getUserId = await User.GetCurrentUserIdAsync();
        var userViewModel = await _mediator.Send(new GetUserProfileQuery() { UserId = getUserId.Id });
        return View(userViewModel);
    }
}