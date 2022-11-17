using iot.Application.Common.ViewModels;
using iot.Domain.Entities.Product;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;

namespace iot.Application.Queries.Devices.FindById;

public class FindByIdDeviceCommand : IRequest<Result<DeviceViewModel>>
{
    public Guid DeviceId { get; set; }
}

public class FindByIdDeviceCommandHandler : IRequestHandler<FindByIdDeviceCommand, Result<DeviceViewModel>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    public FindByIdDeviceCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<Result<DeviceViewModel>> Handle(FindByIdDeviceCommand request, CancellationToken cancellationToken = default)
    {
        var getDevice = await _unitOfWorks.SqlRepository<Device>().TableNoTracking.FirstOrDefaultAsync(a => a.Id == request.DeviceId);
        if (getDevice == null)
            return Result.Fail($"cant find device with id : {request.DeviceId}");

        return getDevice.Adapt<DeviceViewModel>();
    }
}