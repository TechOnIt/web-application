using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Place.DeletePlace;

public class DeletePlaceValidations : BaseFluentValidator<DeletePlaceCommand>
{
    public DeletePlaceValidations()
    {
        RuleFor(a => a.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;
    }
}
