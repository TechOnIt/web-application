using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Sensors;

namespace TechOnIt.Application.Commands.Sensors.CreatSensor;

public class CreateSensorCommand : IRequest<object>, ICommittableRequest
{
    private Guid? _sensorId;
    public Guid Id
    {
        get
        {
            if (_sensorId is null)
                _sensorId = Guid.NewGuid();

            return (Guid)_sensorId;
        }

        set { _sensorId = value; }
    }
    public SensorType? SensorType { get; private set; }
    public Guid GroupId { get; set; }
}

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateSensorCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreateSensorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            request.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;

            Task createSensor = Task.Factory
                .StartNew(() =>
                _unitOfWorks.SensorRepository.CreateSensorAsync(request.Adapt<SensorEntity>(), cancellationToken)
            , cancellationToken);

            await createSensor;

            if (createSensor.IsFaulted)
                return await Task.FromResult(ResultExtention.Failed("error ocurred !"));

            await _mediator.Publish(new SensorNotifications());
            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}