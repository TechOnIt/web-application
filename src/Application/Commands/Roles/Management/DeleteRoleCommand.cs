using iot.Application.Repositories.SQL.Roles;

namespace iot.Application.Commands.Roles.Management;

public class DeleteRoleCommand : Command<Result>
{
    public string Id { get; set; }
}

public class DeleteRoleCommandHandler : CommandHandler<DeleteRoleCommand, Result>
{
    #region DI $ Ctor
    private readonly IUnitOfWorks _unitOfWorks;

    public DeleteRoleCommandHandler(IMediator mediator, IUnitOfWorks unitOfWorks)
        : base(mediator)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    protected override async Task<Result> HandleAsync(DeleteRoleCommand request, CancellationToken cancellationToken)
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