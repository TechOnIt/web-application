using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Relays.DeleteRelay;

public class DeleteRelayValidations : BaseFluentValidator<DeleteRelayCommand>
{
    public DeleteRelayValidations()
    {
        RuleFor(a => a.RelayId)
            .NotNull()
            .NotEqual(Guid.Empty)
            ;
    }
}