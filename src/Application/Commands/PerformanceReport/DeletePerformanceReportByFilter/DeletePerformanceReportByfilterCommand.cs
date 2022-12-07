﻿using TechOnIt.Application.Events.ProductNotifications;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.PerformanceReport.DeletePerformanceReportByFilter;

public class DeletePerformanceReportByfilterCommand : IRequest<Result>, ICommittableRequest
{
    public Expression<Func<Domain.Entities.Product.SensorAggregate.PerformanceReport, bool>> Filter { get; set; }
}

public class DeletePerformanceReportByfilterCommandHandler : IRequestHandler<DeletePerformanceReportByfilterCommand, Result>
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

    public async Task<Result> Handle(DeletePerformanceReportByfilterCommand request, CancellationToken cancellationToken = default)
    {
        var repo = _unitOfWorks.SqlRepository<Domain.Entities.Product.SensorAggregate.PerformanceReport>();

        try
        {
            var mustRemove = await repo.Table.Where(request.Filter).ToListAsync();
            await repo.DeleteRangeAsync(mustRemove);
            await _mediator.Publish(new PerformanceReportNotifications());
            return Result.Ok();
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}