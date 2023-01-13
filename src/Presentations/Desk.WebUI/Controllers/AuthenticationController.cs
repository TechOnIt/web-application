using TechOnIt.Application.Commands.Users.Authentication.SignInCookieCommands;

namespace TechOnIt.Desk.WebUI.Controllers;

public class AuthenticationController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public IActionResult Signin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SignInCookieCommand command, CancellationToken cancellationToken)
    {
        var isSignedin = await _mediator.Send(command, cancellationToken);
        if(!isSignedin)
            return View();
        return Redirect("/");
    }

    [HttpGet]
    public IActionResult Signout()
    {
        return Redirect("/");
    }
}