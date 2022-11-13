using iot.Application.Commands.Users.Authentication.SignInCommands;
using iot.Application.Commands.Users.Authentication.SignInOtpCommands;
using iot.Application.Commands.Users.Authentication.SignUpCommands;
using Org.BouncyCastle.Asn1.Ocsp;

namespace iot.Identity.Api.Controllers.v1;

[Route("v1/[controller]/[action]")]
public class AuthenticationController : BaseController
{
    #region DI & Ctor
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger, IHttpContextAccessor httpContextAccessor)
        : base(mediator)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Command
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Signin([FromBody] SignInUserCommand userLoginInformation)
    {
        _logger.LogWarning($"user with ip : {Request.HttpContext.Connection.RemoteIpAddress} tried to signin");

        return await RunCommandAsyncT(userLoginInformation);
    }

    //[HttpGet]
    //public async Task<IActionResult> GetSignUpOtpMessage([FromBody] SignUpSendOtpCommand userSignUpInformation)
    //    => await RunCommandAsyncT(userSignUpInformation);

    //[HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> SignUpWithOtp([FromBody] SignupUserCommand otpWithPhoneNumber)
    //    => await RunCommandAsyncT(otpWithPhoneNumber);

    [HttpGet]
    public async Task<IActionResult> SignInOtp([FromQuery] SendOtpSmsCommand getSignInOtpCode)
        => await RunCommandAsyncT(getSignInOtpCode);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInOtp([FromBody] SignInWithOtpSmsCommand otpCode)
    => await RunCommandAsyncT(otpCode);
    #endregion
}