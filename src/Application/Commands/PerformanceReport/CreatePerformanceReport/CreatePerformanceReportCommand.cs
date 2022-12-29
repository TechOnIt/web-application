using TechOnIt.Application.Events.ProductNotifications;

namespace TechOnIt.Application.Commands.PerformanceReport.CreatePerformanceReport;

public class CreatePerformanceReportCommand : IRequest<object>
{
    public Guid Id { get; set; }
    public int Value { get; set; }
}

public class CreatePerformanceReportCommandHandler : IRequestHandler<CreatePerformanceReportCommand, object>
{
    #region constructure
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public CreatePerformanceReportCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(CreatePerformanceReportCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Id == Guid.Empty)
                request.Id = Guid.NewGuid();

            Task insertResult = Task.Factory
                .StartNew(() => _unitOfWorks.SensorRepository
                .AddReportToSensorAsync(request.Adapt<TechOnIt.Domain.Entities.Product.SensorAggregate.PerformanceReport>(),cancellationToken)
            ,cancellationToken);

            await _mediator.Publish(new PerformanceReportNotifications());

            return await Task.FromResult(ResultExtention.IdResult(request.Id));
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}