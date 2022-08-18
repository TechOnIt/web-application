namespace iot.Application.Commands.Users.Management.SetUserPassword;

public class SetUserPasswordCommandValidation : BaseFluentValidator<SetPasswordUserCommand>
{
    public SetUserPasswordCommandValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(300)
            ;

        RuleFor(user => user.RepeatPassword)
            .Equal(user => user.Password)
            ;
    }
}
