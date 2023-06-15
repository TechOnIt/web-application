using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Devices.DeleteDevice;

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