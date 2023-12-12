using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Devices.UpdateDevice;

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
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public UpdateDeviceCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<object> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var updateResult =
                Task.Factory
                .StartNew(() => _unitOfWorks.DeviceRepositry.UpdateAsync(request.Adapt<Domain.Entities.RelayEntity>(), cancellationToken)
                , cancellationToken);

            Task.WaitAny(updateResult);

            if (updateResult.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed("device can not be found !"));

            await _mediator.Publish(new DeviceNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.DeviceId));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}