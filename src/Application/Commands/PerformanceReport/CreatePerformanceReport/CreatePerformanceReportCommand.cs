using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;

namespace TechOnIt.Application.Commands.PerformanceReport.CreatePerformanceReport;

public class CreatePerformanceReportCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public int Value { get; set; }
}

public class CreatePerformanceReportCommandHandler : IRequestHandler<CreatePerformanceReportCommand, Result<Guid>>
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
    public async Task<Result<Guid>> Handle(CreatePerformanceReportCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Id == Guid.Empty)
                request.Id = Guid.NewGuid();

            await _unitOfWorks.SqlRepository<Domain.Entities.Product.SensorAggregate.PerformanceReport>()
                .AddAsync(new Domain.Entities.Product.SensorAggregate.PerformanceReport(request.Id, request.Value, DateTime.Now));

            await _mediator.Publish(new PerformanceReportNotifications());
            return Result.Ok(request.Id);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}