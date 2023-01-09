using TechOnIt.Application.Common.Models.ViewModels.Devices;

namespace TechOnIt.Application.Queries.Devices.GetAllByFilter;

public class GetAllDeviceByFilterCommand : IRequest<object>
{
}

public class GetAllDeviceByFilterCommandHandler : IRequestHandler<GetAllDeviceByFilterCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorksl;
    public GetAllDeviceByFilterCommandHandler(IUnitOfWorks unitOfWorksl)
    {
        _unitOfWorksl = unitOfWorksl;
    }
    #endregion

    public async Task<object> Handle(GetAllDeviceByFilterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var allDevices = await _unitOfWorksl.DeviceRepositry.GetAllDevicesAsync(cancellationToken);
            return allDevices.Adapt<IList<DeviceViewModel>>();
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}