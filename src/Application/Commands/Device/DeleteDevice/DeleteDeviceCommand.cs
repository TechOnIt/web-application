using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.Device.DeleteDevice;

public class DeleteDeviceCommand : IRequest<object>
{
    public Guid DeviceId { get; set; }
}

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, object>
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

    public async Task<object> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Task getDevice = Task.Factory.StartNew(() => _unitOfWorks.DeviceRepositry.DeleteByIdAsync(request.DeviceId, cancellationToken), cancellationToken);
            Task.WaitAny(getDevice);

            if(!getDevice.IsCompletedSuccessfully) 
                return await Task.FromResult(ResultExtention.Failed($"can not find device with Id : {request.DeviceId}"));

            await _mediator.Publish(new DeviceNotifications());

            return await Task.FromResult(ResultExtention.BooleanResult(true));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}