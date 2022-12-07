using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Services.ProductServices.ProductContracts;

namespace TechOnIt.Application.Commands.Device.DeleteDevice;

public class DeleteDeviceCommand : IRequest<Result>
{
    public Guid DeviceId { get; set; }
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Result>
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

    public async Task<Result> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var isExists = await _deviceService.FindByIdAsNoTrackingAsync(request.DeviceId, cancellationToken);
            if (isExists is null)
                return Result.Fail($"can not find device with Id : {request.DeviceId}");

            bool getDevice = await _deviceService.DeleteByIdAsync(request.DeviceId, cancellationToken);
            if (!getDevice)
                return Result.Fail("an error occured !");

            await _mediator.Publish(new DeviceNotifications());
            return Result.Ok();
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}