using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Common.Security.JwtBearer;

namespace iot.Application.Commands.Users.Authentication.SignInCommands;

public class SignInUserCommand : IRequest<Result<AccessToken>>
{
    public string? Username { get; set; } // phonenumber
    public string? Password { get; set; }
}


public sealed class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Result<AccessToken>>
{
    #region messages
    const string NotFound = "Not found user !";
    const string WrongInformations = "Username or password is wrong!";
    const string LockUser = "user is locked !";
    const string WrongPassowrd = "password is wrong!";
    #endregion

    #region DI & Ctor
    private readonly IUnitOfWorks _unitOfWorks;
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;

    public SignInUserCommandHandler(IUnitOfWorks unitOfWorks, IMediator mediator, IJwtService jwtService)
    {
        _unitOfWorks = unitOfWorks;
        _mediator = mediator;
        _jwtService = jwtService;
    }
    #endregion

    public async Task<Result<AccessToken>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWorks.UserRepository.FindUserByPhoneNumberWithRolesAsyncNoTracking(request.Username,cancellationToken);

        if (user == null) return Result.Fail(NotFound);
        else if (user.IsBaned is true) return Result.Fail(WrongInformations);
        else if (user.LockOutDateTime != null) return Result.Fail(LockUser);
        else if (user.Password != PasswordHash.Parse(request.Password)) return Result.Fail(WrongPassowrd);

        AccessToken token = await _jwtService.GenerateAccessToken(user, cancellationToken);
        return Result.Ok(new AccessToken());

        if (token.Token is null)
            return Result.Fail("");

        await _mediator.Publish(new SignInUserNotifications()); // notification events
        return Result.Ok(token);
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