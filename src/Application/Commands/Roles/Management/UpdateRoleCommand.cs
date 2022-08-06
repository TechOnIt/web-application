using iot.Application.Repositories.SQL.Roles;

namespace iot.Application.Commands.Roles.Management;

public class UpdateRoleCommand : Command<Result>
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class UpdateRoleCommandHandler : CommandHandler<UpdateRoleCommand, Result>
{
    #region DI $ Ctor
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandHandler(IMediator mediator, IRoleRepository roleRepository)
        : base(mediator)
    {
        _roleRepository = roleRepository;
    }
    #endregion

    protected override async Task<Result> HandleAsync(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Find role by id.
        var roleId = Guid.Parse(request.Id);
        var role = await _roleRepository.GetByIdAsync(cancellationToken, roleId);
        if (role == null)
            return Result.Fail("Role was not found!");

        // Map role name.
        role.SetName(request.Name);
        bool saveWasSucceded = await _roleRepository.UpdateAsync(role, saveNow: true, cancellationToken);
        if (saveWasSucceded == false)
        {
            // TODO:
            // Add log error.
            return Result.Fail("an error was occured.");
        }
        return Result.Ok();
    }
}

public class UpdateRoleCommandValidator : BaseFluentValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;

        RuleFor(r => r.Name)
            .NotEmpty()
            .Matches(RegexConstant.EnglishAlphabet)
            .MaximumLength(50)
            ;
    }
}