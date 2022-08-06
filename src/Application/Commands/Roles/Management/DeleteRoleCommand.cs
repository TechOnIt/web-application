using iot.Application.Repositories.SQL.Roles;

namespace iot.Application.Commands.Roles.Management;

public class DeleteRoleCommand : Command<Result>
{
    public string Id { get; set; }
}

public class DeleteRoleCommandHandler : CommandHandler<DeleteRoleCommand, Result>
{
    #region DI $ Ctor
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleCommandHandler(IMediator mediator, IRoleRepository roleRepository)
        : base(mediator)
    {
        _roleRepository = roleRepository;
    }
    #endregion

    protected override async Task<Result> HandleAsync(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        // Find role by id.
        var roleId = Guid.Parse(request.Id);
        var role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);
        if (role == null)
            return Result.Fail("Role was not found!");

        bool saveWasSucceded = await _roleRepository.DeleteAsync(role, saveNow: true, cancellationToken);
        if (saveWasSucceded == false)
        {
            // TODO:
            // Add log error.
            return Result.Fail("an error was occured.");
        }
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