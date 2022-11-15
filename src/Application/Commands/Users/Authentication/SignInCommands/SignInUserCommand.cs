using iot.Application.Common.ViewModels.Users.Authentication;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserCommand : IRequest<Result<AccessToken>>
{
    public string? Username { get; set; } // phonenumber
    public string? Password { get; set; }
}


public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AccessToken>>
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

    public async Task<Result<AccessToken>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var signInUserResult = await _identityService.SignInUserAsync(request.Username,request.Password,cancellationToken);
        if (!signInUserResult.HasValue)
            return Result.Fail("Server side error has occured.");

        if (signInUserResult.Value.Token is null)
            return Result.Fail(signInUserResult.Value.Message);

        await _mediator.Publish(new SignInUserNotifications()); // notification events
        return Result.Ok(signInUserResult.Value.Token);
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