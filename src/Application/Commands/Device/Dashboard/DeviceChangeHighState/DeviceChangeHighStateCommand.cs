using TechOnIt.Infrastructure.Repositories.SQL.Devices;

namespace TechOnIt.Application.Commands.Device.Dashboard.DeviceChangeHighState;

public class DeviceChangeHighStateCommand : IRequest<bool>
{
    public Guid DeviceId { get; set; }
    public bool IsHigh { get; set; }
}

public class DeviceChangeHighStateCommandHandler : IRequestHandler<DeviceChangeHighStateCommand, bool>
{
    #region Ctor
    private readonly IUnitOfWorks _unitOfWork;

    public DeviceChangeHighStateCommandHandler(IUnitOfWorks unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion

    public async Task<bool> Handle(DeviceChangeHighStateCommand request, CancellationToken cancellationToken)
    {
        var device = await _unitOfWork.DeviceRepositry.FindByIdAsync(request.DeviceId, cancellationToken);
        if (device is null)
            return false;

        device.IsHigh = request.IsHigh;
        await _unitOfWork.DeviceRepositry.UpdateAsync(device, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        return true;
    }
}