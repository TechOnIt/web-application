using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TechOnIt.Application.Queries.Users.Dashboard.ProfileQueries;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Authorize]
[Area("Dashboard")]
public class ProfileController : Controller
{
    #region DI / Ctor

    private IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public ProfileController(IMediator mediator,
        IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
    }

    #endregion

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken stoppingToken)
    {
        var getUserId = await User.GetCurrentUserIdAsync();
        var userViewModel = await _mediator.Send(new GetUserProfileQuery() { UserId = getUserId.Id }, stoppingToken);
        return View(userViewModel);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CancellationToken stoppingToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> VerifyPhoneNumber(CancellationToken stoppingToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> VerifyPhoneNumber(string code, CancellationToken stoppingToken)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> VerifyEmail(CancellationToken stoppingToken)
    {
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> ChangePassword(CancellationToken stoppingToken)
    {
        return Ok();
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Signout()
    {
        if (_httpContext.HttpContext is not null)
            await _httpContext.HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/");
    }
}