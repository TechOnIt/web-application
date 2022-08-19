using iot.Application.Repositories.UnitOfWorks.Identity;

namespace iot.Application.Commands.Roles.Management.UpdateRole;

public class UpdateRoleCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    #region DI $ Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public UpdateRoleCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Find role by id.
        var roleId = Guid.Parse(request.Id);
        var role = await _unitOfWorks.SqlRepository<Role>().GetByIdAsync(cancellationToken, roleId);
        if (role == null)
            return Result.Fail("Role was not found!");

        // Map role name.
        role.SetName(request.Name);
        await _unitOfWorks.SqlRepository<Role>().UpdateAsync(role, cancellationToken);
        return Result.Ok();
    }
}