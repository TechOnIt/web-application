﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TechOnIt.Application.Commands.Users.Authentication.SignInCookieCommands;

namespace TechOnIt.Admin.Web.Controllers;

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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SignInCookieCommand command, CancellationToken cancellationToken)
    {
        var signinResult = await _mediator.Send(command, cancellationToken);
        if (!signinResult.IsSuccess)
            return View();
        return Redirect("/");
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
