using iot.Application.Events.ProductNotifications;

namespace iot.Application.Commands.Device.DeleteDevice;

public class DeleteDeviceCommand : IRequest<Result>
{
    public Guid DeviceId { get; set; }
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Result>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public DeleteDeviceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<Result> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.Device>();

        try
        {
            var getDevice = await repo.Table.FirstOrDefaultAsync(a=>a.Id==request.DeviceId);
            if (getDevice == null)
                return Result.Fail($"can not find device with Id : {request.DeviceId}");
            else
            {
                await repo.DeleteAsync(getDevice);

                await _mediator.Publish(new DeviceNotifications());

                return Result.Ok();
            }
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}