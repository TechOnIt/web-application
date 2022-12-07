using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Users.Management.ForceDelete;

public class ForceDeleteUserValidation : BaseFluentValidator<ForceDeleteUserCommand>
{
    public ForceDeleteUserValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}
