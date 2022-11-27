namespace iot.Application.Commands.PerformanceReport.DeletePerformanceReportByFilter;

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