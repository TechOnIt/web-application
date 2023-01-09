using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Queries.Devices.FindById;

public class FindByIdDeviceCommand : IRequest<object>
{
    public Guid DeviceId { get; set; }
}

public class FindByIdDeviceCommandHandler : IRequestHandler<FindByIdDeviceCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public FindByIdDeviceCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<object> Handle(FindByIdDeviceCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var getDevice = await _unitOfWorks.DeviceRepositry.FindByIdAsNoTrackingAsync(request.DeviceId, cancellationToken);

            if (getDevice is null)
                return ResultExtention.NotFound($"cant find device with id : {request.DeviceId}");

            return getDevice.Adapt<DeviceViewModel>();
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}