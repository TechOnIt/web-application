using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users.Queries.Commands;

public class AllUsersCommand : IRequest<IList<UserViewModel>>
{
    public Expression<Func<User, bool>> ExpressionCondition { get; set; }
}
