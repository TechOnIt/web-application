using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.PerformanceReport.CreatePerformanceReport;

public class CreatePerformanceReportValidations : BaseFluentValidator<CreatePerformanceReportCommand>
{
    public CreatePerformanceReportValidations()
    {
        RuleFor(a => a.Value)
            .NotNull()
            .NotEmpty()
            .NotEqual(0)
            ;
    }
}
