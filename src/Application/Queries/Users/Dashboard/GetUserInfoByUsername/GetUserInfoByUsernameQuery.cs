using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.Dashboard.GetUserInfoByUsername;

public class GetUserInfoByUsernameQuery : IRequest<UserViewModel?>
{
    public string username { get; set; }
}

public class GetUserInfoByUsernameQueryHandler : IRequestHandler<GetUserInfoByUsernameQuery, UserViewModel?>
{
    #region Ctor
    private readonly IUserReports _userReports;

    public GetUserInfoByUsernameQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<UserViewModel?> Handle(GetUserInfoByUsernameQuery request, CancellationToken cancellationToken)
       => await _userReports.FindByUsernameNoTrackAsViewModelAsync(request.username, cancellationToken);
}