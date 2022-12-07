using TechOnIt.Application.Commands.Roles.Management.UpdateRole;
using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Roles.Management.DeleteRole;

public class DeleteRoleCommandValidator : BaseFluentValidator<UpdateRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}