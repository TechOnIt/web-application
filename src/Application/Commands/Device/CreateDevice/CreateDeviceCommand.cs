using iot.Application.Common.Interfaces;
using iot.Application.Common.ViewModels.Devices;
using iot.Application.Events.ProductNotifications;
using iot.Application.Services.ProductServices.ProductContracts;
using iot.Domain.Enums;
using Mapster;

namespace iot.Application.Commands.Device.CreateDevice;

public class CreateDeviceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Result<Guid>>
{
    #region Constructure
    private readonly IDeviceService _deviceService;
    private readonly IMediator _mediator;

    public CreateDeviceCommandHandler(IDeviceService deviceService, IMediator mediator)
    {
        _deviceService = deviceService;
        _mediator = mediator;
    }


    #endregion

    public async Task<Result<Guid>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = new Domain.Entities.Product.Device(request.Pin, request.DeviceType, request.IsHigh, request.PlaceId);
            var createResult = await _deviceService.CreateDeviceAsync(model.Adapt<DeviceViewModel>(), cancellationToken);

            await _mediator.Publish(new DeviceNotifications());

            if (createResult is null)
                return Result.Fail("an error occured !");
            else
                return Result.Ok(createResult.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}