namespace TechOnIt.Application.Commands.Groups.UpdateGroup;

public class UpdateGroupValidations : BaseFluentValidator<UpdateGroupCommand>
{
    public UpdateGroupValidations()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            ;

        RuleFor(a => a.StuctureId)
            .NotNull()
            ;

        RuleFor(a => a.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            ;

        RuleFor(a => a.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(150)
            ;
    }
}