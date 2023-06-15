namespace TechOnIt.Application.Commands.SensorReports.CreateSensorReport;

public class CreateSensorReportValidations : BaseFluentValidator<CreateSensorReportCommand>
{
    public CreateSensorReportValidations()
    {
        RuleFor(a => a.Value)
            .NotNull()
            .NotEmpty()
            .NotEqual(0)
            ;
    }
}
