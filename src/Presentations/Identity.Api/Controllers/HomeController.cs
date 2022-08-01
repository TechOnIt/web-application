using iot.Application.Commands.LoginHistories;
using iot.Application.Commands.Users;
using iot.Application.Queries.Users.FindById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
<<<<<<< HEAD
    public async Task<IActionResult> CreateUser(UserCreateCommand command)
=======
    public async Task<IActionResult> CreateUser([FromBody] UserCreateCommand command)
>>>>>>> eb60dc9a9f9614f85e11ee40ec4d02d492ffbaf1
    {
        var result = await _mediator.Send(new UserCreateCommand
        {
            PhoneNumber = command.PhoneNumber,
            Email = command.Email,
            Password = command.Password,
            Name = command.Name,
            Surname = command.Surname
        });

        return Ok(result);
    }

    [HttpGet]
    [Route("Home/FindUserById/{Id}")]
    public async Task<IActionResult> FindUserById([FromRoute] Guid Id)
    {
        var result = await _mediator.Send(new FindUserByIdQuery
        {
            Id= Id
        });

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        var createLoginHistoryResult = await _mediator.Send(new LoginHistoryCreateCommand
        {
            Ip = "192.168.1.100"
        });

        return Ok();
    }
}