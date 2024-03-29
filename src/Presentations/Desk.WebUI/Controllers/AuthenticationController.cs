﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TechOnIt.Application.Commands.Users.Authentication.SignInCookieCommands;

namespace TechOnIt.Desk.WebUI.Controllers;

public class AuthenticationController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContext;

    public AuthenticationController(IMediator mediator,
        IHttpContextAccessor httpContext)
    {
        _mediator = mediator;
        _httpContext = httpContext;
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
    public async Task<IActionResult> Signin(SignInCookieCommand command, CancellationToken cancellationToken)
    {
        var isSignedin = await _mediator.Send(command, cancellationToken);
        if (!isSignedin)
        {
            TempData["signin-summery"] = "Username or password is wrong!";
            return View(command);
        }
        return Redirect("/dashboard/structure");
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