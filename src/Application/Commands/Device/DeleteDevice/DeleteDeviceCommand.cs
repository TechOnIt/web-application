using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Device.DeleteDevice;

public class DeleteDeviceCommand : IRequest<object>
{
    public Guid DeviceId { get; set; }
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, object>
{
    #region constructure
    private readonly IDeviceService _deviceService;
    private readonly IMediator _mediator;
    public DeleteDeviceCommandHandler(IDeviceService deviceService, IMediator mediator)
    {
        _deviceService = deviceService;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool? getDevice = await _deviceService.DeleteByIdAsync(request.DeviceId, cancellationToken);
            if(getDevice is null) return ResultExtention.Failed($"can not find device with Id : {request.DeviceId}");

            await _mediator.Publish(new DeviceNotifications());
            return ResultExtention.BooleanResult(getDevice);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}