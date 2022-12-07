using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserValidation : BaseFluentValidator<UnBanUserCommand>
{
    public UnBanUserValidation()
    {
        RuleFor(u => u.Id)
    .NotEmpty()
    .Matches(RegexConstant.Guid)
    .MaximumLength(100)
    ;
    }
}
