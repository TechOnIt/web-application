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
    public async Task<IActionResult> On(string deviceId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceChangeHighStateCommand
        {
            DeviceId = Guid.Parse(deviceId),
            IsHigh = true
        }, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Off(string deviceId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceChangeHighStateCommand
        {
            DeviceId = Guid.Parse(deviceId),
            IsHigh = false
        }, cancellationToken);
        return Ok();
    }
}
