using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Relays.UpdateRelay;

public class UpdateRelayValidations : BaseFluentValidator<UpdateRelayCommand>
{
    public UpdateRelayValidations()
    {
        RuleFor(a => a.RelayId)
            .NotNull()
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;

        RuleFor(a => a.RelayType)
            .NotEmpty()
            .NotNull()
            ;

        RuleFor(a => a.Pin)
            .NotNull()
            .NotEqual(0)
            ;

    }
}