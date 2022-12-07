using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using Mapster;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Common.Models.ViewModels.Devices;
using TechOnIt.Application.Services.ProductServices.ProductContracts;

namespace TechOnIt.Application.Commands.Device.CreateDevice;

public class CreateDeviceCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public int Pin { get; set; }
    public DeviceType DeviceType { get; private set; }
    public bool IsHigh { get; set; }
    public Guid PlaceId { get; set; }
}

public class CreateDeviceCommandHandler : IRequestHandler<CreateDeviceCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(CreateDeviceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = new Domain.Entities.Product.Device(request.Pin, request.DeviceType, request.IsHigh, request.PlaceId);
            var createResult = await _deviceService.CreateAsync(model.Adapt<DeviceViewModel>(), cancellationToken);

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