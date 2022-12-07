using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Roles.Management.UpdateRole;

public class UpdateRoleCommandValidator : BaseFluentValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;

        RuleFor(r => r.Name)
            .NotEmpty()
            .Matches(RegexConstant.EnglishAlphabet)
            .MaximumLength(50)
            ;
    }
}