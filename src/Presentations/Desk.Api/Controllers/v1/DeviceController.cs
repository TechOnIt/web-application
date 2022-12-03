using iot.Application.Commands.Device.CreateDevice;
using iot.Application.Commands.Device.DeleteDevice;
using iot.Application.Commands.Device.UpdateDevice;
using iot.Application.Common.Frameworks.ApiResultFrameWork.Filters;

namespace iot.Desk.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
	#region constructor
	private readonly IMediator _mediator;
	public DeviceController(IMediator mediator)
	{
		_mediator = mediator;
	}

    #endregion

    [HttpPost]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateDeviceCommand device, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(device, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromBody] UpdateDeviceCommand deviceCommand, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(deviceCommand,cancellationToken);
        return Ok(result);
    }


    [HttpDelete]
    [ApiResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid deviceId)
    {
        var result = await _mediator.Send(new DeleteDeviceCommand { DeviceId=deviceId});
        return Ok(result);
    }
}
