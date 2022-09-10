namespace iot.Application.Commands.Device.DeleteDevice;

public class DeleteDeviceValidations : BaseFluentValidator<DeleteDeviceCommand>
{
	public DeleteDeviceValidations()
	{
		RuleFor(a => a.DeviceId)
			.NotNull()
			.NotEqual(Guid.Empty)
			;
	}
}
