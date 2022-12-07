using iot.Application.Common.Models.ViewModels.Users.Authentication;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Commands.Users.Authentication.SignInOtpCommands;

public class VerifyOtpSmsCommand : IRequest<Result<AccessToken>>
{
    public string OtpCode { get; set; }
    public string PhoneNumber { get; set; }
}

public class VerifyOtpSmsCommandHandler : IRequestHandler<VerifyOtpSmsCommand, Result<AccessToken>>
{
    #region DI & Ctor
    private readonly IIdentityService _identityService;

    public VerifyOtpSmsCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    #endregion

    public async Task<Result<AccessToken>> Handle(VerifyOtpSmsCommand request, CancellationToken cancellationToken = default)
    {
        var accessToke = await _identityService.VerifySignInOtpAsync(request.OtpCode, request.PhoneNumber, cancellationToken);
        if (accessToke.Token.Token is null)
            return Result.Fail(accessToke.Message);

        return Result.Ok(accessToke.Token);
    }
}

public class VerifyOtpSmsCommandValidations : BaseFluentValidator<VerifyOtpSmsCommand>
{
    public VerifyOtpSmsCommandValidations()
    {
        RuleFor(a => a.OtpCode)
            .NotNull()
            .NotEmpty()
            .Length(4)
            ;

        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Length(11)
            ;
    }
}