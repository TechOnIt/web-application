using Microsoft.AspNetCore.Authorization;
using TechOnIt.Application.Queries.Users.GetAllUsers;

namespace TechOnIt.Admin.Api.Areas.Controllers.v1;

[ApiController]
[Area("manage")]
[Route("v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    #region DI & Ctor's
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Queries
    [Authorize]
    [HttpGet, ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> GetAll([FromQuery] GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    #endregion 

    #region Commands
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> ResetPassword([FromBody] ResetUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> Ban([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BanUserCommand() { UserId = id }, cancellationToken);
        return Ok(result);
    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> UnBan([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UnBanUserCommand() { Id = id }, cancellationToken);
        return Ok(result);
    }



    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> RemoveAccount([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RemoveUserAccountCommand() { UserId = id }, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public async Task<IActionResult> ForceDelete([FromRoute] string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ForceDeleteUserCommand() { Id = id }, cancellationToken);
        return Ok(result);
    }
    #endregion
}