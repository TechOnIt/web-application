using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Queries.PerformanceReports.FindPerformanceReportByDateTime;

public class FindPerformanceReportByDateTimeValidations : BaseFluentValidator<FindPerformanceReportByDateTimeQuery>
{
    public FindPerformanceReportByDateTimeValidations()
    {
        RuleFor(a => a.MinDate)
            .NotNull()
            .NotEmpty()
            .NotEqual(DateTime.MinValue)
            ;
    }
}
