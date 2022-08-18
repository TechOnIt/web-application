namespace iot.Application.Commands.Users.Management.UnBanUser;

public class UnBanUserValidation : BaseFluentValidator<UnBanUserCommand>
{
    public UnBanUserValidation()
    {
        RuleFor(u => u.Id)
    .NotEmpty()
    .Matches(RegexConstant.Guid)
    .MaximumLength(100)
    ;
    }
}
