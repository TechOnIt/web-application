namespace TechOnIt.Application.Commands.Roles.Management.AssignToUserRole;

public class AssignRoleToUserCommandValidator : AbstractValidator<AssignRoleToUserCommand>
{
    public AssignRoleToUserCommandValidator()
    {
        RuleFor(artu => artu.RoleId)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            ;
        RuleFor(artu => artu.UserId)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            ;
    }
}