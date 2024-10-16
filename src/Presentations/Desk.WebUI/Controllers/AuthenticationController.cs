using TechOnIt.Application.Commands.Users.Authentication.SignInCookieCommands;

namespace TechOnIt.Desk.Web.Controllers;

public class AuthenticationController : Controller
{
    #region DI / Ctor

    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    [HttpGet]
    public IActionResult Signin()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
            return Redirect("/");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SignInCookieCommand command,
        CancellationToken cancellationToken)
    {
        var signinResult = await _mediator.Send(command, cancellationToken);
        if (!signinResult.IsSuccess)
        {
            TempData["signin-summery"] = "Username or password is wrong!";
            return View(command);
        }
        return Redirect("/dashboard/structure");
    }

}