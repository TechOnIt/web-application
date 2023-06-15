using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Users.Management.RemoveUserAccount;

public class RemoveUserAccountCommandValidation : BaseFluentValidator<RemoveUserAccountCommand>
{
    public RemoveUserAccountCommandValidation()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotNull()
            ;
    }
}
