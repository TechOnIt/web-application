using TechOnIt.Application.Commands.Relays.Dashboard.RelayChangeHighState;

namespace TechOnIt.Desk.Web.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class RelayController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public RelayController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> On(string deviceId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RelayChangeHighStateCommand
        {
            RelayId = Guid.Parse(deviceId),
            IsHigh = true
        }, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Off(string deviceId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RelayChangeHighStateCommand
        {
            RelayId = Guid.Parse(deviceId),
            IsHigh = false
        }, cancellationToken);
        return Ok();
    }
}