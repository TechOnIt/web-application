using iot.Application.Commands.Users.Management.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace iot.Identity.Api.Controllers.v1;

[Route("[controller]/[action]")]
[ApiController]
public class HomeController : BaseController
{
    #region DI & Ctor's
    public HomeController(IMediator mediator)
        : base(mediator) { }
    #endregion

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        => await RunCommandAsync(command);

    //[HttpGet]
    //[Route("Home/FindUserById/{Id}")]
    //public async Task<IActionResult> FindUserById([FromRoute] FindUserByIdQuery query)
    //    => await RunQueryAsync(query);
}