namespace iot.Application.Queries.PerformanceReports.FindPerformanceReportById;

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
