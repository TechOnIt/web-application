using iot.Application.Commands.LoginHistories;
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