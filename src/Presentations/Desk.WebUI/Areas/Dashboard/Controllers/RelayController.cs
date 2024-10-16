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
    public async Task<IActionResult> On(string relayId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RelayChangeHighStateCommand
        {
            RelayId = Guid.Parse(relayId),
            IsHigh = true
        }, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Off(string relayId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RelayChangeHighStateCommand
        {
            RelayId = Guid.Parse(relayId),
            IsHigh = false
        }, cancellationToken);
        return Ok();
    }
}