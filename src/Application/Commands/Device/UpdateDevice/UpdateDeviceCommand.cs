using TechOnIt.Application.Common.Models.ViewModels.Devices;
using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Device.UpdateDevice;

public class UpdateDeviceCommand : IRequest<object>, ICommittableRequest
{
    public Guid DeviceId { get; set; }
    public int Pin { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, object>
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

    public async Task<object> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var viewModel = new DeviceViewModel(request.DeviceId, request.PlaceId, request.Pin, request.DeviceType, request.IsHigh);
            var updateResult = await _deviceService.UpdateAsync(viewModel, cancellationToken);

            if (updateResult is null)
                return ResultExtention.Failed("device can not be found !");

            await _mediator.Publish(new DeviceNotifications());

            return ResultExtention.IdResult(updateResult.Id);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}