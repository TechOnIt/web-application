using iot.Application.Common.ViewModels.Users;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Commands.Users.Authentication.SignUpCommands;

public class SignUpSendOtpCommand : IRequest<Result<string>>
{
    public string Phonenumber { get; set; }
    public string Password { get; set; }
}

public class SignUpSendOtpCommandHandler : IRequestHandler<SignUpSendOtpCommand, Result<string>>
{
    #region constructor
    private readonly IIdentityService _identityService;
    public SignUpSendOtpCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    #endregion

    public async Task<Result<string>> Handle(SignUpSendOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newUser = new UserViewModel(request.Phonenumber,request.Phonenumber,request.Password);
            var signUpresult = await _identityService.SignUpAndSendOtpCode(newUser,cancellationToken);

            if (signUpresult.Code == 0)
                return Result.Fail(signUpresult.Message);

            return Result.Ok(signUpresult.Message);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}

public class SignUpSendOtpCommandValidations : BaseFluentValidator<SignUpSendOtpCommand>
{
    public SignUpSendOtpCommandValidations()
    {
        RuleFor(a => a.Phonenumber)
            .NotEmpty()
            .NotNull()
            .MinimumLength(11)
            .MaximumLength(11)
            ;

        RuleFor(a => a.Password)
            .NotEmpty()
            .NotNull()
            .Length(8)
            ;
    }
}