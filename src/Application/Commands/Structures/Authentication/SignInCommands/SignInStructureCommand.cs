using TechOnIt.Application.Services.Authenticateion.StructuresService;

namespace TechOnIt.Application.Commands.Structures.Authentication.SignInCommands;

public class SignInStructureCommand : IRequest<object>
{
    public string ApiKey { get; set; }
    public string Password { get; set; }
}

public class SignInStructureCommandHandler : IRequestHandler<SignInStructureCommand, object>
{
    #region DI & Ctor
    private readonly IStructureService _structureService;

    public SignInStructureCommandHandler(IStructureService structureService)
    {
        _structureService = structureService;
    }
    #endregion

    public async Task<object> Handle(SignInStructureCommand request, CancellationToken cancellationToken = default)
        => await _structureService.SignInAsync(request.ApiKey, request.Password, cancellationToken);
}

public sealed class SignInStructureCommandValidator : BaseFluentValidator<SignInStructureCommand>
{
    public SignInStructureCommandValidator()
    {
        RuleFor(u => u.ApiKey)
            .NotEmpty()
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            // TODO:
            // Uncomment this for strong password!
            //.Matches(RegexConstant.Password)
            ;
    }
}