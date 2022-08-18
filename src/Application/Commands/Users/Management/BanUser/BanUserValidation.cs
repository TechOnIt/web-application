namespace iot.Application.Commands.Users.Management.BanUser;

public class BanUserValidation : BaseFluentValidator<BanUserCommand>
{
    public BanUserValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
    ;
    }
}
