using TechOnIt.Application.Common.Models.DTOs.Users.Authentication;
using TechOnIt.Application.Services.Authenticateion.AuthenticateionContracts;

namespace TechOnIt.Application.Commands.Users.Authentication.SignUpCommands;

public class SignUpSendOtpCommand : IRequest<object>
{
    public string Phonenumber { get; set; }
    public string Password { get; set; }
}

public class SignUpSendOtpCommandHandler : IRequestHandler<SignUpSendOtpCommand, object>
{
    #region constructor
    private readonly IIdentityService _identityService;
    public SignUpSendOtpCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    #endregion

    public async Task<object> Handle(SignUpSendOtpCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var newUser = new CreateUserDto(request.Phonenumber, request.Phonenumber, request.Password);
            var signUpresult = await _identityService.SignUpAndSendOtpCode(newUser, cancellationToken);

            if(signUpresult.Code is null)
                return ResultExtention.Failed(signUpresult.Status.ToString());

            return ResultExtention.OtpResult(signUpresult.Code);
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
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