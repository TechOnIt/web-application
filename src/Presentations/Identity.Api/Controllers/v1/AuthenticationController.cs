using iot.Application.Commands.Users.Authentication.SignInCommands;
using iot.Application.Commands.Users.Authentication.SignUpCommands;

namespace iot.Identity.Api.Controllers.v1;

[Route("v1/[controller]/[action]")]
public class AuthenticationController : BaseController
{
    #region DI & Ctor
    public AuthenticationController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Command

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Signin([FromBody] SignInUserCommand userLoginInformation)
        => await RunCommandAsyncT(userLoginInformation);

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SignOut()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignupUserCommand userSignUpInformation)
        =>await RunCommandAsyncT(userSignUpInformation);

    #endregion

    #region Queries

    #endregion
}
