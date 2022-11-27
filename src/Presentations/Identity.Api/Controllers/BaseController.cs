namespace iot.Identity.Api.Controllers;

[ApiController]
[Route("v1/[area]/[controller]/[action]")]
public class BaseController : ControllerBase
{
    #region DI & Ctor's
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Commands
    protected async Task<IActionResult> RunCommandAsyncT<TRequest>(TRequest request)
        where TRequest : class
    {
        var commandResult = await _mediator.Send(request);

        if (commandResult is not null)
            return Ok(commandResult);

        return BadRequest();
    }

    protected async Task<IActionResult> RunCommandAsync<TRequest>(TRequest request)
        where TRequest : ICommittableRequest
    {
        var result = await _mediator.Send(request) as Result<Guid>;

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest();
    }
    #endregion

    #region Queries
    protected async Task<IActionResult> RunQueryAsync<TQuery>(TQuery request)
        where TQuery : class
    {
        var result = await _mediator.Send(request);
        if (result is not null)
            return Ok(result);
        return BadRequest();
    }
    #endregion
}
