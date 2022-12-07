using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Queries.Users.GetAllUsers;

public class GetUsersQueryValidations : BaseFluentValidator<GetUsersQuery>
{
    public GetUsersQueryValidations()
    {
        //RuleFor(a => a.PhoneNumber)
        //	.Length(11)
        //	;

        //RuleFor(a => a.Email)
        //	.MinimumLength(10)
        //	;
    }
}
