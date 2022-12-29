namespace TechOnIt.Application.Commands.PerformanceReport.DeletePerformanceReportByFilter;

public class DeletePerformanceReportByfilterValidations : BaseFluentValidator<DeletePerformanceReportByfilterCommand>
{
    public DeletePerformanceReportByfilterValidations()
    {
        RuleFor(a => a.SensorId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}