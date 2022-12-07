using TechOnIt.Application.Common.Extentions;
using TechOnIt.Application.Common.Interfaces;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Roles.Management.CreateRole;

public class CreateRoleCommand : IRequest<Result<string>>, ICommittableRequest
{
    public string Name { get; set; }
}

public class CreateRoleCommandHanlder : IRequestHandler<CreateRoleCommand, Result<string>>
{
    #region DI & Ctor
    private readonly IRoleService _roleService;

    public CreateRoleCommandHanlder(IRoleService roleService)
    {
        _roleService = roleService;
    }
    #endregion

    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _roleService.CreateRoleAsync(new Role(request.Name), cancellationToken);
            if (result.Result.IsDuplicate())
                return Result.Fail(result.Message);

            return Result.Ok(result.Message);

        }
        catch (Exception exp)
        {
            if (exp.InnerException != null)
                return Result.Fail($"innerException : {exp.InnerException.Message} - exception :{exp.Message}");
            else
                return Result.Fail(exp.Message);
        }
    }
}