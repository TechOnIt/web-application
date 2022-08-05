using iot.Application.Repositories.SQL.Roles;

namespace iot.Application.Commands.Roles.Management;

public class CreateRoleCommand : Command<Result<Guid>>
{
    public string Name { get; set; }
}

public class CreateRoleCommandHanlder : CommandHandler<CreateRoleCommand, Result<Guid>>
{
    #region DI & Ctor
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHanlder(IMediator mediator, IRoleRepository roleRepository)
        : base(mediator)
    {
        _roleRepository = roleRepository;
    }
    #endregion

    protected override async Task<Result<Guid>> HandleAsync(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return Result.Ok();
    }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .MinimumLength(3)
            .Matches(RegexConstant.EnglishAlphabet)
            ;
    }
}