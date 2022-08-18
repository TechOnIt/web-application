namespace iot.Application.Commands.Users.RemoveUserAccount;

public class RemoveUserAccountCommandValidation : BaseFluentValidator<RemoveUserAccountCommand>
{
    public RemoveUserAccountCommandValidation()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .Matches(RegexConstant.Guid)
            .MaximumLength(100)
            ;
    }
}
