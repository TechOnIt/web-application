using TechOnIt.Application.Commands.Device.Dashboard.DeviceChangeHighState;

namespace TechOnIt.Desk.WebUI.Areas.Dashboard.Controllers;

[Area("Dashboard")]
public class DeviceController : Controller
{
    #region Ctor
    private readonly IMediator _mediator;

    public DeviceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    [HttpGet]
    public async Task<IActionResult> On(string id, string structureId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceChangeHighStateCommand
        {
            DeviceId = Guid.Parse(id),
            IsHigh = true
        }, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Off(string id, string structureId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceChangeHighStateCommand
        {
            DeviceId = Guid.Parse(id),
            IsHigh = false
        }, cancellationToken);
        return Ok();
    }
}
