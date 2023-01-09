using TechOnIt.Application.Common.Constants;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Commands.Users.Management.SetUserPassword;

public class SetUserPasswordCommandValidation : BaseFluentValidator<SetUserPasswordCommand>
{
    public SetUserPasswordCommandValidation()
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