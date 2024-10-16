using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Enums;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Sensors.UpdateSensor;

public class UpdateSensorCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public Guid Id { get; set; }
    public SensorType? SensorType { get; private set; }
    public Guid GroupId { get; set; }
}

public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand, object>
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

    public async Task<object> Handle(UpdateSensorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var updateSensor =
                Task.Factory
                .StartNew(() =>
                _unitOfWorks.SensorRepository.UpdateSensorAsync(request.Adapt<Domain.Entities.SensorAggregate.SensorEntity>(), cancellationToken)
                , cancellationToken);
            await updateSensor;

            await _mediator.Publish(new SensorNotifications());



            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}