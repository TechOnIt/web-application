using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Sensor.CreatSensor
{
    public class CreateSensorValidations : BaseFluentValidator<CreateSensorCommand>
    {
        public CreateSensorValidations()
        {
            RuleFor(a => a.SensorType)
                .NotNull()
                ;

            RuleFor(a => a.PlaceId)
                .NotEmpty()
                .NotNull()
                ;
        }
    }
}
