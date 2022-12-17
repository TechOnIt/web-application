using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using Mapster;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Commands.Device.CreateDevice;

public class CreateDeviceCommand : IRequest<object>, ICommittableRequest
{
    public int Pin { get; set; }
    public DeviceType DeviceType { get; set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, object>
{
    #region Ctor
    private readonly IDeviceService _deviceService;
    private readonly IMediator _mediator;

    public CreateDeviceCommandHandler(IDeviceService deviceService, IMediator mediator)
    {
        _deviceService = deviceService;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = new Domain.Entities.Product.Device(request.Pin, request.DeviceType, request.IsHigh, request.PlaceId);
            var createResult = await _deviceService.CreateAsync(model.Adapt<DeviceViewModel>(), cancellationToken);

            await _mediator.Publish(new DeviceNotifications());

            if (createResult is null)
                return ResultExtention.Failed("an error occured !");
            else
                return ResultExtention.IdResult(createResult.Id);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}