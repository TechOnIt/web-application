using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Groups.CreateGroup;

public class CreateGroupValidations : BaseFluentValidator<CreateGroupCommand>
{
    public CreateGroupValidations()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(40)
            ;

        RuleFor(a => a.StuctureId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(150)
            ;
    }
}
