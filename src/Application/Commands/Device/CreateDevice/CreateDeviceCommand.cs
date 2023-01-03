using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using TechOnIt.Application.Common.Interfaces;

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
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateDeviceCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks)
    {
        _mediator = mediator;
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var model = new Domain.Entities.Product.Device(request.Pin, request.DeviceType, request.IsHigh, request.PlaceId);
            Task createResult = Task.Factory.StartNew(() => _unitOfWorks.DeviceRepositry.CreateAsync(model, cancellationToken), cancellationToken);
            Task.WaitAny(createResult);

            await _mediator.Publish(new DeviceNotifications());

            if (!createResult.IsCompletedSuccessfully)
                return ResultExtention.Failed("an error occured !");
            else
                return ResultExtention.IdResult(model.Id);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}