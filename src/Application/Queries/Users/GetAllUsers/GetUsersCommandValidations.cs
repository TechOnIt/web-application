namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersCommandValidations : BaseFluentValidator<GetUsersCommand>
{
	public GetUsersCommandValidations()
	{
		//RuleFor(a => a.PhoneNumber)
		//	.Length(11)
		//	;

		//RuleFor(a => a.Email)
		//	.MinimumLength(10)
		//	;
	}
}
