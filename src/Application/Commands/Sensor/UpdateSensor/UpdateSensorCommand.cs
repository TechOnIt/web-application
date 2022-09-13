using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;
using iot.Domain.Enums;

namespace iot.Application.Commands.Sensor.UpdateSensor;

public class UpdateSensorCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
    public SensorType? SensorType { get; private set; }
    public Guid PlaceId { get; set; }
}

public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand, Result<Guid>>
{

    #region constrocture
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public UpdateSensorCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<Result<Guid>> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.Sensor>();
        try
        {
            var sesnor = await repo.Table.FirstOrDefaultAsync(a=>a.Id==request.Id);
            if (sesnor is null)
                return Result.Fail($"can not find sesnsor with id : {request.Id}");

            sesnor.PlaceId=request.PlaceId;
            sesnor.SetSensorType(request.SensorType);

            await repo.UpdateAsync(sesnor);
            await _mediator.Publish(new SensorNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}