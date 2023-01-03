using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserCommand : IRequest<object>
{
    public string? Username { get; set; } // phonenumber
    public string? Password { get; set; }
}

public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, object>
{
    #region DI & Ctor
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public SignInUserCommandHandler(IMediator mediator, IIdentityService identityService)
    {
        _identityService = identityService;
        _mediator = mediator;
    }
    #endregion

    public async Task<object> Handle(SignInUserCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var signInUserResult = await _identityService.SignInUserAsync(request.Username, request.Password, cancellationToken);

            if (!signInUserResult.HasValue)
                return ResultExtention.Failed("Server side error has occured.");

            if (signInUserResult.Value.Token is null)
                return ResultExtention.Failed(signInUserResult.Value.Message);

            await _mediator.Publish(new SignInUserNotifications()); // notification events
            return signInUserResult.Value.Token;
        }
        catch (Exception exp)
        {
            throw new AppException(exp.Message);
        }
    }
}

public sealed class SignInUserCommandValidator : BaseFluentValidator<SignInUserCommand>
{
    public SignInUserCommandValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(IdentitySettingConstant.MinimumUsernameLength)
            .MaximumLength(IdentitySettingConstant.MaximumUsernameLength)
            ;

        RuleFor(u => u.Password)
            .MinimumLength(IdentitySettingConstant.MinimumPasswordLength)
            .MaximumLength(IdentitySettingConstant.MaximumPasswordLength)
            // TODO:
            // Uncomment this for strong password!
            //.Matches(RegexConstant.Password)
            ;
    }
}