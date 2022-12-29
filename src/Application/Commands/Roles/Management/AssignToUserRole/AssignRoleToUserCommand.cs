using TechOnIt.Application.Common.Enums.IdentityService;
using TechOnIt.Application.Reports.Roles;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Roles.Management.AssignToUserRole;

public class AssignRoleToUserCommand : IRequest<Result>
{
    public string RoleId { get; set; }
    public string UserId { get; set; }
}

public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, Result>
{
    #region DI & Ctor
    private readonly IRoleService _roleService;
    private readonly IRoleReports _roleReports;
    private readonly IUserService _userService;
    public AssignRoleToUserCommandHandler(IRoleService roleService,
        IRoleReports roleReports,
        IUserService userService)
    {
        _roleService = roleService;
        _roleReports = roleReports;
        _userService = userService;
    }
    #endregion

    public async Task<Result> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        // Find role.
        Guid roleId = Guid.Parse(request.RoleId);
        var role = await _roleReports.FindByIdAsync(roleId, cancellationToken);
        // Validation is null.
        if (role == null)
            return Result.Fail("User not found.");

        // Find user.
        Guid userId = Guid.Parse(request.UserId);
        var user = await _userService.FindUserByIdAsync(userId, cancellationToken);
        // Validation is null.
        if (user == null)
            return Result.Fail("User not found.");

        // Assign role to user.
        var result = await _roleService.AssignToUser(role, user, cancellationToken);

        if (result.Result == IdentityCrudStatus.Succeeded)
            return Result.Ok();
        return Result.Fail(result.Message);
    }
}