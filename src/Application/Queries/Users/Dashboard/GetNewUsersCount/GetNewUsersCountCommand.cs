using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.Dashboard.GetNewUsersCount
{
    public class GetNewUsersCountCommand : IRequest<object>
    {
    }

    public class GetNewUsersCountCommandHandler : IRequestHandler<GetNewUsersCountCommand, object>
    {
        #region Ctor
        private readonly IUserReports _userReports;

        public GetNewUsersCountCommandHandler(IUserReports userReports)
        {
            _userReports = userReports;
        }
        #endregion

        public async Task<object> Handle(GetNewUsersCountCommand request, CancellationToken cancellationToken)
        {
            // TODO:
            // Complete this command and query.
            var result = await _userReports.GetNewUsersCountGroupbyRegisterDateAsync(
                from: DateTime.Now.AddMonths(-6), cancellationToken);
            return result;
        }
    }
}
