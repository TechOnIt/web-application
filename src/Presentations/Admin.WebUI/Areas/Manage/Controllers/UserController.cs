﻿using TechOnIt.Application.Commands.Users.Management.CreateUser;
using TechOnIt.Application.Commands.Users.Management.ResetUserPassword;
using TechOnIt.Application.Commands.Users.Management.UpdateUser;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Queries.Users.Dashboard.GetUserInfoById;
using TechOnIt.Application.Queries.Users.GetAllUsers;

namespace TechOnIt.Admin.Web.Areas.Manage.Controllers;

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
    public async Task<IActionResult> Edit(string id, CancellationToken cancellationToken)
    {
        var userViewModel = await _mediator.Send(new GetUserInfoByIdQuery()
        {
            Id = id
        }, cancellationToken);
        if (userViewModel == null)
        {
            // NotFound
        }
        return View(userViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateUserCommand command, CancellationToken stoppingToken)
    {
        var updateUserCommandResult = await _mediator.Send(command, stoppingToken);
        return Redirect("/manage/user/edit/" + command.UserId);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromForm] ResetUserPasswordCommand command, CancellationToken stoppingToken)
    {
        var resetUserPasswordCommandResult = await _mediator.Send(command, stoppingToken);
        return Redirect("/manage/user/edit/" + command.UserId);
    }
}