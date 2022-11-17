using iot.Application.Common.DTOs.Users.Authentication;
using iot.Application.Events.IdentityNotifications.Authentication;
using iot.Application.Services.Authenticateion.AuthenticateionContracts;

namespace iot.Application.Commands.Users.Authentication.SignUpCommands;

public sealed class SignupUserCommand : IRequest<Result<AccessToken>>
{
    public string? PhoneNumber { get; set; }
    public string OtpCode { get; set; }
}

internal sealed class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, Result<AccessToken>>
{
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;

    public SignupUserCommandHandler(IMediator mediator, IIdentityService identityService)
    {
        _mediator = mediator;
        _identityService = identityService;
    }

    public async Task<Result<AccessToken>> Handle(SignupUserCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var signupResult = await _identityService.SignUpWithOtpAsync(request.PhoneNumber, request.OtpCode,cancellationToken);
            if (signupResult.Token.Token is null)
                return Result.Fail(signupResult.Message);

            await _mediator.Publish(new SignUpUserNotifications(request.PhoneNumber,string.Empty));
            return Result.Ok(signupResult.Token);
        }
        catch (Exception exp)
        {
            return Result.Fail($"error : {exp.Message}");
        }
    }
}

public class SignupUserCommandValidator : BaseFluentValidator<SignupUserCommand>
{
    public SignupUserCommandValidator()
    {
        RuleFor(u => u.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .Length(11)
            ;

        RuleFor(a => a.OtpCode)
            .NotNull()
            .NotEmpty()
            .Length(4)
            ;
    }
}