namespace iot.Application.Commands.Users.Authentication;

public sealed class SignupUserCommand : IRequest<Result>
{
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? RepeatPassword { get; set; }
}

internal sealed class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, Result>
{
    private readonly IUnitOfWorks _unitOfWorks;
    public SignupUserCommandHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    public async Task<Result> Handle(SignupUserCommand request, CancellationToken cancellationToken)
    {
        // Check username is unique?
        bool userIsExist = await _unitOfWorks.SqlRepository<User>()
            .IsExistsAsync(u => u.Username == request.PhoneNumber);
        if (userIsExist)
            return Result.Fail("Username not exist.");

        return Result.Ok();
    }
}

internal sealed class SignupUserCommandValidator : BaseFluentValidator<SignupUserCommand>
{
    public SignupUserCommandValidator()
    {
        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .Length(11)
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            // TODO:
            // Uncomment this for strong password! :)
            .Matches(RegexConstant.Password)
            .MinimumLength(IdentitySettingConstant.MinimumPasswordLength)
            .MaximumLength(IdentitySettingConstant.MaximumPasswordLength)
            ;

        RuleFor(u => u.RepeatPassword)
            .NotEmpty()
            .Equal(u => u.Password)
            ;
    }
}