using iot.Application.Common.Frameworks.ApiResultFrameWork.Filters;
using iot.Application.Queries.Users.GetAllUsers;

namespace iot.Identity.Api.Areas.Manage.Controllers.v1;

[Area("Manage"), Route("v1/[area]/[controller]/[action]")]
public class UserController : BaseController
{
    #region DI & Ctor's
    public UserController(IMediator mediator)
        : base(mediator)
    {
    }
    #endregion

    #region Commands
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    => await RunCommandAsync(command);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        => await RunCommandAsync(command);


    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SetPassword([FromBody] SetUserPasswordCommand command)
        => await RunCommandAsync(command);

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Ban([FromRoute] string id)
        => await RunCommandAsync(new BanUserCommand() { Id = id });

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> UnBan([FromRoute] string id)
        => await RunCommandAsync(new UnBanUserCommand() { Id = id });


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> RemoveAccount([FromRoute] string id)
        => await RunCommandAsync(new RemoveUserAccountCommand() { Id = id });

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ForceDelete([FromRoute] string id)
        => await RunCommandAsync(new ForceDeleteUserCommand() { Id = id });
    #endregion

    #region Queries
    [HttpGet, ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
     => await RunQueryAsync(query);
    #endregion
}