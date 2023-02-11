using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Roles.Management.DeleteRole;

public class DeleteRoleCommand : IRequest<object>, ICommittableRequest
{
    public Guid RoleId { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, object>
{
    #region DI $ Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public DeleteRoleCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(DeleteRoleCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var role = await _unitOfWorks.RoleRepository.FindRoleByIdAsync(request.RoleId, cancellationToken);
            if (role == null) 
                return ResultExtention.NotFound($"can not find role with id : {request.RoleId}");

            await _unitOfWorks.RoleRepository.DeleteRoleAsync(role, cancellationToken);
            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}