using iot.Application.Common.Interfaces;
using iot.Application.Repositories.UnitOfWorks.Identity;

namespace iot.Application.Commands.Roles.Management.DeleteRole;

public class DeleteRoleCommand : IRequest<Result>, ICommittableRequest
{
    public string Id { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    #region DI $ Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public DeleteRoleCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        // Find role by id.
        var roleId = Guid.Parse(request.Id);
        var role = await _unitOfWorks.SqlRepository<Role>().GetByIdAsync(cancellationToken, roleId);
        if (role == null)
            return Result.Fail("Role was not found!");

        await _unitOfWorks.SqlRepository<Role>().DeleteAsync(role, cancellationToken);
        return Result.Ok();
    }
}