namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersCommandValidations : BaseFluentValidator<GetUsersQuery>
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
