﻿namespace iot.Application.Commands.Sensor.UpdateSensor;

public class UpdateSensorValidations : BaseFluentValidator<UpdateSensorCommand>
{
	public UpdateSensorValidations()
	{
		RuleFor(a => a.Id)
			.NotEmpty()
			.NotNull()
			;

		RuleFor(a => a.SensorType)
			.NotNull()
			;

		RuleFor(a => a.PlaceId)
			.NotEmpty()
			.NotNull()
			;
	}
}