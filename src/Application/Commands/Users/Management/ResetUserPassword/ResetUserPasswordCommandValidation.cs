namespace TechOnIt.Application.Commands.Users.Management.ResetUserPassword;

public class ResetUserPasswordCommandValidation : BaseFluentValidator<ResetUserPasswordCommand>
{
    public ResetUserPasswordCommandValidation()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotNull();

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