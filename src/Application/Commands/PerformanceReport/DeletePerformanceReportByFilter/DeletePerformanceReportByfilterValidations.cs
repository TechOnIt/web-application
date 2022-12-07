using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.PerformanceReport.DeletePerformanceReportByFilter;

public class DeletePerformanceReportByfilterValidations : BaseFluentValidator<DeletePerformanceReportByfilterCommand>
{
    public DeletePerformanceReportByfilterValidations()
    {
        RuleFor(a => a.Filter)
            .NotNull()
            .NotEmpty()
            ;
    }
}