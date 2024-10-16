using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Groups.DeleteGroup;

public class DeleteGroupValidations : BaseFluentValidator<DeleteGroupCommand>
{
    public DeleteGroupValidations()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;
    }
}
