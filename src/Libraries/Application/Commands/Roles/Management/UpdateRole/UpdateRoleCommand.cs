namespace TechOnIt.Application.Commands.Roles.Management.UpdateRole;

public class UpdateRoleCommand : IRequest<object>, ICommittableRequest
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, object>
{
    #region DI $ Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public UpdateRoleCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(UpdateRoleCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var role = await _unitOfWorks.RoleRepository.FindRoleByIdAsync(request.RoleId, cancellationToken);
            if (role == null) 
                return ResultExtention.NotFound($"can not find role with id : {request.RoleId}");

            role.SetName(request.Name);
            await _unitOfWorks.RoleRepository.UpdateRoleAsync(role, cancellationToken);
            return ResultExtention.BooleanResult(true);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
}