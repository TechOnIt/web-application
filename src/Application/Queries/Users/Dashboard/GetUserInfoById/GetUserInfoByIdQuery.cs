using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.Dashboard.GetUserInfoById;

public class FindUserViewModelByIdQuery : IRequest<UserViewModel?>
{
    public Guid Id { get; set; }
}

public class FindUserViewModelByIdQueryHandler : IRequestHandler<FindUserViewModelByIdQuery, UserViewModel?>
{
    #region Ctor
    private readonly IUserReports _userReports;

    public FindUserViewModelByIdQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<UserViewModel?> Handle(FindUserViewModelByIdQuery request, CancellationToken cancellationToken)
       => await _userReports.FindByIdNoTrackAsViewModelAsync(request.Id, cancellationToken);
}