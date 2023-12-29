using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Relays.CreateRelay;

public class CreateRelayValidations : BaseFluentValidator<CreateRelayCommand>
{
    public CreateRelayValidations()
    {
        RuleFor(a => a.PlaceId)
            .NotNull()
            .NotEmpty()
            ;

        RuleFor(a => a.Pin)
            .NotNull()
            .NotEqual(0)
            ;

        RuleFor(a => a.RelayType)
            .NotNull()
            .NotEmpty()
            ;
    }
}