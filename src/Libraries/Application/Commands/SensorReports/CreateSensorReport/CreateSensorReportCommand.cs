using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Domain.Entities.Sensors;

namespace TechOnIt.Application.Commands.SensorReports.CreateSensorReport;

public class CreateSensorReportCommand : IRequest<object>
{
    public Guid Id { get; set; }
    public int Value { get; set; }
}

public class CreateSensorReportCommandHandler : IRequestHandler<CreateSensorReportCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreateSensorReportCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreateSensorReportCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Id == Guid.Empty)
                request.Id = Guid.NewGuid();

            Task insertResult = Task.Factory
                .StartNew(() => _unitOfWorks.SensorRepository
                .AddReportToSensorAsync(request.Adapt<SensorReportEntity>(), cancellationToken)
            , cancellationToken);

            await _mediator.Publish(new SensorReportNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}