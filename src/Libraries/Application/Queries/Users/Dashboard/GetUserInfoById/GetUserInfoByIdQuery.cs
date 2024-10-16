using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.Dashboard.GetUserInfoById;

public class GetUserInfoByIdQuery : IRequest<UserViewModel?>
{
    public string Id { get; set; }
}

public class GetUserInfoByUsernameQueryHandler : IRequestHandler<GetUserInfoByIdQuery, UserViewModel?>
{
    #region Ctor
    private readonly UserReports _userReports;

    public GetUserInfoByUsernameQueryHandler(UserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<UserViewModel?> Handle(GetUserInfoByIdQuery request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(request.Id);
        return await _userReports.FindByIdNoTrackAsViewModelAsync(userId, cancellationToken);
    }
}