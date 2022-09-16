using iot.Application.Common.Interfaces;
using iot.Application.Events.ProductNotifications;
using iot.Domain.Enums;
namespace iot.Application.Commands.Sensor.CreatSensor;

public class CreateSensorCommand : IRequest<Result<Guid>>,ICommittableRequest
{
    public Guid Id { get; set; }
    public SensorType? SensorType { get; private set; }
    public Guid PlaceId { get; set; }
}

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, Result<Guid>>
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

    public async Task<Result<Guid>> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null || request.Id == Guid.Empty)
                request.Id = Guid.NewGuid();

            await _unitOfWorks.SqlRepository<iot.Domain.Entities.Product.Sensor>()
                .AddAsync(new iot.Domain.Entities.Product.Sensor(request.Id,request.SensorType,request.PlaceId));

            await _mediator.Publish(new SensorNotifications());

            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}