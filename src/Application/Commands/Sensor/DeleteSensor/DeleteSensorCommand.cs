using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;
using iot.Infrastructure.Repositories.UnitOfWorks;

namespace iot.Application.Commands.Sensor.DeleteSensor;

public class DeleteSensorCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
}

public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand, Result<Guid>>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public DeleteSensorCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<Result<Guid>> Handle(DeleteSensorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.SensorAggregate.Sensor>();

            var sesnsor = await repo.Table.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (sesnsor is null)
            {
                return Result.Fail($"can not find sessor with id : {sesnsor}");
            }

            await repo.DeleteAsync(sesnsor);
            await _mediator.Publish(new SensorNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}