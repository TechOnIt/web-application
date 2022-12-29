using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.PerformanceReport.DeletePerformanceReportByFilter;

public class DeletePerformanceReportByfilterCommand : IRequest<bool>, ICommittableRequest
{
    public Guid SensorId { get; set; }
}

public class DeletePerformanceReportByfilterCommandHandler : IRequestHandler<DeletePerformanceReportByfilterCommand, bool>
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    public DeletePerformanceReportByfilterCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }

    #endregion

    public async Task<bool> Handle(DeletePerformanceReportByfilterCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            Task cleareAllReports =
                Task.Factory.StartNew(() =>
                _unitOfWorks.SensorRepository
                .ClearReportsBySensorIdAsync(request.SensorId, cancellationToken), cancellationToken);

            Task.WaitAny(cleareAllReports);

            await _mediator.Publish(new PerformanceReportNotifications());

            return await Task.FromResult(true);
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}