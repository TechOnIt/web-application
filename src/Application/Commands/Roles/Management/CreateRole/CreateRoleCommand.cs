using iot.Application.Common.Interfaces;
using iot.Application.Repositories.UnitOfWorks.Identity;

namespace iot.Application.Commands.Roles.Management.CreateRole;

public class CreateRoleCommand : IRequest<Result<Guid>>, ICommittableRequest
{
    public string Name { get; set; }
}

public class CreateRoleCommandHanlder : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public CreateRoleCommandHanlder(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        // TODO:
        // Complete command.
        return Result.Ok();
    }
}