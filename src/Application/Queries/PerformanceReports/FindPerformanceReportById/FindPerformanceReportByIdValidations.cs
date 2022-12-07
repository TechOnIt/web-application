using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Queries.PerformanceReports.FindPerformanceReportById;

public class FindPerformanceReportByIdValidations : BaseFluentValidator<FindPerformanceReportByIdCommand>
{
    public FindPerformanceReportByIdValidations()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;
    }
}
