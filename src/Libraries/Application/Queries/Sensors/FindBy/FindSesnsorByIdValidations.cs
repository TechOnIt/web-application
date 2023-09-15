namespace TechOnIt.Application.Queries.Sensors.FindBy;

public class FindSesnsorByIdValidations : BaseFluentValidator<FindSesnsorByIdQuery>
{
    public FindSesnsorByIdValidations()
    {
        RuleFor(a => a.SensorId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;
    }
}
