namespace iot.Application.Commands.Sensor.DeleteSensor;

public class DeleteSensorValidations : BaseFluentValidator<DeleteSensorCommand>
{
	public DeleteSensorValidations()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotNull()
			;
	}
}
