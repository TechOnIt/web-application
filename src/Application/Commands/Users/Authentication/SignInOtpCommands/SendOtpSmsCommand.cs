using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Users.Authentication.SignInOtpCommands;

public class SendOtpSmsCommand : IRequest<Result<string>>
{
    public string PhoneNumber { get; set; }
}

public class SignInOtpCommandHandler : IRequestHandler<SendOtpSmsCommand, Result<string>>
{
    #region constructor
    private readonly IIdentityService _identityService;
    public SignInOtpCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    #endregion

    public async Task<Result<string>> Handle(SendOtpSmsCommand request, CancellationToken cancellationToken = default)
    {
        var otpCode = await _identityService.SendOtpAsync(request.PhoneNumber, cancellationToken);
        if (otpCode.Code is null)
            return Result.Fail(otpCode.Message);

        return Result.Ok("sms has been send");
    }
}

public class SendOtpSmsCommandValidations : BaseFluentValidator<SendOtpSmsCommand>
{
    public SendOtpSmsCommandValidations()
    {
        RuleFor(a => a.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .MaximumLength(11)
            .MinimumLength(11)
            ;
    }
}