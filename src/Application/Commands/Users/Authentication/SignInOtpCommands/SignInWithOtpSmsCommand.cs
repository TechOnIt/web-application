using iot.Application.Common.ViewModels.Users.Authentication;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Commands.Users.Authentication.SignInOtpCommands;

public class SignInWithOtpSmsCommand : IRequest<Result<AccessToken>>
{
    public int OtpCode { get; set; }
    public string PhoneNumber { get; set; }
}

public class SignInWithOtpSmsCommandHandler : IRequestHandler<SignInWithOtpSmsCommand, Result<AccessToken>>
{
    #region constructor
    private readonly IIdentityService _identityService;
    public SignInWithOtpSmsCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    #endregion

    public async Task<Result<AccessToken>> Handle(SignInWithOtpSmsCommand request, CancellationToken cancellationToken)
    {
        var accessToke = await _identityService.SignInUserWithOtpAsync(request.OtpCode,request.PhoneNumber,cancellationToken);
        if (accessToke.Token.Token is null)
            return Result.Fail(accessToke.Message);

        return Result.Ok(accessToke.Token);
    }
}

public class SignInWithOtpSmsCommandValidations : BaseFluentValidator<SignInWithOtpSmsCommand>
{
    public SignInWithOtpSmsCommandValidations()
    {
        RuleFor(a => a.OtpCode)
            .NotNull()
            .NotEqual(0)
            .LessThan(9001)
            ;

        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(11)
            .MaximumLength(11)
            ;
    }
}