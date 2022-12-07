using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Sensor.DeleteSensor;

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
