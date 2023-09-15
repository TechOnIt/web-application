using TechOnIt.Application.Common.Constants;

namespace TechOnIt.Application.Commands.Roles.Management.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .MinimumLength(3)
            .Matches(RegexConstant.EnglishAlphabet)
            ;
    }
}