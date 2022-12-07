using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Place.UpdatePlace;

public class UpdatePlaceValidations : BaseFluentValidator<UpdatePlaceCommand>
{
    public UpdatePlaceValidations()
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
