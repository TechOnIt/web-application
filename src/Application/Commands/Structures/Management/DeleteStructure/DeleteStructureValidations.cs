using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Structures.Management.DeleteStructure;

public class DeleteStructureValidations : BaseFluentValidator<DeleteStructureCommand>
{
    public DeleteStructureValidations()
    {
        RuleFor(a => a.StructureId)
            .NotNull()
            .NotEmpty()
            ;
    }
}
