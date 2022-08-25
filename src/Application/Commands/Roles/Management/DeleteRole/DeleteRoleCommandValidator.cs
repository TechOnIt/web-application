using iot.Application.Commands.Roles.Management.UpdateRole;

namespace iot.Application.Commands.Roles.Management.DeleteRole;

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