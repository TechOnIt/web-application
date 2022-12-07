using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Device.CreateDevice;

public class CreateDeviceValidations : BaseFluentValidator<CreateDeviceCommand>
{
    public CreateDeviceValidations()
    {
        RuleFor(a => a.PlaceId)
            .NotNull()
            .NotEmpty()
            ;

        RuleFor(a => a.Pin)
            .NotNull()
            .NotEqual(0)
            ;

        RuleFor(a => a.DeviceType)
            .NotNull()
            .NotEmpty()
            ;
    }
}