using iot.Application.Common.Interfaces;
using iot.Application.Common.ViewModels.Devices;
using iot.Application.Events.ProductNotifications;
using iot.Domain.Enums;

namespace iot.Application.Commands.Device.UpdateDevice;

public class UpdateDeviceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public Guid DeviceId { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Result<Guid>>
{
    #region Constructure
    private readonly IDeviceService _deviceService;
    private readonly IMediator _mediator;
    public UpdateDeviceCommandHandler(IDeviceService deviceService, IMediator mediator)
    {
        _deviceService = deviceService;
        _mediator = mediator;
    }

    #endregion
    public async Task<Result<Guid>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var viewModel = new DeviceViewModel(request.DeviceId, request.PlaceId, request.Pin, request.DeviceType, request.IsHigh);
            var updateResult = await _deviceService.UpdateAsync(viewModel, cancellationToken);

            if (updateResult is null)
                return Result.Fail("device can not be found !");

            await _mediator.Publish(new DeviceNotifications());
            return Result.Ok(updateResult.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}