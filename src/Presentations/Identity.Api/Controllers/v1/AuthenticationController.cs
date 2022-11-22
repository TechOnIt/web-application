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

    [HttpPost]
    public async Task<IActionResult> SignInOtp([FromBody] SendOtpSmsCommand getSignInOtpCode)
        => await RunCommandAsyncT(getSignInOtpCode);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifySignInOtp([FromBody] VerifyOtpSmsCommand otpCode)
    => await RunCommandAsyncT(otpCode);
    #endregion
}