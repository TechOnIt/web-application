namespace TechOnIt.Application.Commands.Users.Authentication.SignUpWithSignInCommands;

public class FullSignUpCommandValidation : BaseFluentValidator<FullSignUpCommand>
{
	public FullSignUpCommandValidation()
	{
		RuleFor(a => a.Username)
			.NotNull()
			.NotEmpty()
			.MinimumLength(2)
			.MaximumLength(50);

		RuleFor(a => a.Password)
			.NotNull()
			.NotEmpty()
			.MinimumLength(4)
			.MaximumLength(50);
	}
}
