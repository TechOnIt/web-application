using iot.Application.Commands.Users.Authentication.SignInCommands;
using iot.Application.Commands.Users.Authentication.SignInOtpCommands;
using iot.Application.Commands.Users.Authentication.SignUpCommands;

namespace iot.Identity.Api.Controllers.v1;

[Route("v1/[controller]/[action]")]
public class AuthenticationController : BaseController
{
    #region DI & Ctor
    public AuthenticationController(IMediator mediator)
        : base(mediator) { }
    #endregion

    #region Command
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Signin([FromBody] SignInUserCommand userLoginInformation)
        => await RunCommandAsyncT(userLoginInformation);

    [HttpGet]
    public async Task<IActionResult> GetSignUpOtpMessage([FromBody] SignUpSendOtpCommand userSignUpInformation)
        => await RunCommandAsyncT(userSignUpInformation);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpWithOtp([FromBody] SignupUserCommand otpWithPhoneNumber)
        => await RunCommandAsyncT(otpWithPhoneNumber);

    [HttpGet]
    public async Task<IActionResult> SignInOtp([FromBody] SendOtpSmsCommand getSignInOtpCode)
        => await RunCommandAsyncT(getSignInOtpCode);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInOtp([FromBody] SignInWithOtpSmsCommand otpCode)
    => await RunCommandAsyncT(otpCode);
    #endregion

    #region Queries

    #endregion
}
