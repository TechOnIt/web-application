using TechOnIt.Application.Queries.Users.GetAllUsers;
using Org.BouncyCastle.Asn1.Ocsp;
using TechOnIt.Identity.Api.Controllers;

namespace TechOnIt.Identity.Api.Areas.Manage.Controllers.v1;

[Area("manage")]
public class UserController : BaseController
{
    #region DI & Ctor's
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
        : base(mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Queries
    [HttpGet, ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
     => await ExecuteAsync(query, cancellationToken);
    #endregion

    #region Commands
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    => await ExecuteAsync(command);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        => await ExecuteAsync(command);

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SetPassword([FromBody] SetUserPasswordCommand command)
        => await ExecuteAsync(command);

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Ban([FromRoute] string id)
        => await ExecuteAsync(new BanUserCommand() { Id = id });

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> UnBan([FromRoute] string id)
        => await ExecuteAsync(new UnBanUserCommand() { Id = id });


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> RemoveAccount([FromRoute] string id)
        => await ExecuteAsync(new RemoveUserAccountCommand() { Id = id });

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ForceDelete([FromRoute] string id)
        => await ExecuteAsync(new ForceDeleteUserCommand() { Id = id });
    #endregion
}