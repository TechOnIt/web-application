namespace iot.Application.Commands.Structures.Management.UpdateStructure;

public class UpdateStructureValidators : BaseFluentValidator<UpdateStructureCommand>
{
    public UpdateStructureValidators()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(100)
            ;

        RuleFor(a => a.Type)
            .NotEmpty()
            .NotNull()
            ;

        RuleFor(a=>a.Id)
            .NotEmpty()
            .NotNull()
            ;
    }
}
