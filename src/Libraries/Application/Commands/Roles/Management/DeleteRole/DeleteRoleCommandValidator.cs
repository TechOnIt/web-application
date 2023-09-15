using TechOnIt.Application.Commands.Roles.Management.UpdateRole;

namespace TechOnIt.Application.Commands.Roles.Management.DeleteRole;

public class DeleteRoleCommandValidator : BaseFluentValidator<UpdateRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(r => r.RoleId)
            .NotEmpty()
            .NotNull()
            ;
    }
}