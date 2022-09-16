namespace iot.Application.Commands.PerformanceReport.CreatePerformanceReport;

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
