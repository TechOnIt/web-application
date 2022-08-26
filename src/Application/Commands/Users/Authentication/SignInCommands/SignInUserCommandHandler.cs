using iot.Application.Common.DTOs.Users.Authentication;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AccessToken>>
{
    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;

    public SignInUserCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
    }
    #endregion

    public async Task<Result<AccessToken>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var signinPassword = await _unitOfWorks.UserRepository.UserSignInByPasswordAsync(request.Username, request.Password, cancellationToken);

        if(signinPassword.Token is null)
            return Result.Fail(signinPassword.Message);

        await _mediator.Publish(new SignInUserNotifications()); // notification events
        return Result.Ok(signinPassword.Token);
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
