using TechOnIt.Application.Common.Interfaces;

namespace TechOnIt.Application.Commands.Roles.Management.CreateRole;

public class CreateRoleCommand : IRequest<object>, ICommittableRequest
{
    public string Name { get; set; }
}

public class CreateRoleCommandHanlder : IRequestHandler<CreateRoleCommand, object>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public CreateRoleCommandHanlder(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<object> Handle(CreateRoleCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            if(await _unitOfWorks.RoleRepository.IsExistsRoleNameAsync(request.Name, cancellationToken) == true)
                return Result.Fail("Duplicate: role name already exists in system");

            Role newRole = new Role(request.Name);
            await _unitOfWorks.RoleRepository.CreateRoleAsync(newRole, cancellationToken);
            return ResultExtention.IdResult(newRole.Id);
        }
        catch (Exception exp)
        {
            return ResultExtention.Failed(exp.Message);
        }
    }
}