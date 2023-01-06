using TechOnIt.Application.Commands.Users.Authentication.SignInCommands;
using TechOnIt.Application.Commands.Users.Authentication.SignInOtpCommands;

namespace TechOnIt.Identity.Api.Controllers.v1;

[ApiController]
[Route("v1/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    #region DI & Ctor
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mediator = mediator;
    }
    #endregion

    #region Command
    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Signin([FromBody] SignInUserCommand userLoginInformation, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"user with ip : {Request.HttpContext.Connection.RemoteIpAddress} tried to signin");
        var result = await _mediator.Send(userLoginInformation,cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInOtp([FromBody] SendOtpSmsCommand getSignInOtpCode, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(getSignInOtpCode, cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifySignInOtp([FromBody] VerifyOtpSmsCommand otpCode)
    {
        var result = await _mediator.Send(otpCode);
        return Ok(result);
    }
    #endregion
}