using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Reports.Users;

namespace iot.Application.Queries.Roles.GetAllRoles;

public class GetRolesQuery : PaginatedSearchWithSize, IRequest<PaginatedList<UserViewModel>>
{
}

public class GetAllRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<UserViewModel>>
{
    #region constructor
    private readonly IUserReports _userReports;
    public GetAllRolesQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<PaginatedList<UserViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken = default)
    {
        var result = new PaginatedList<UserViewModel>();
        try
        {
            result = await _userReports.GetAllPaginatedSearchAsync<UserViewModel>(new PaginatedSearchWithSize
            {
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = request.PageSize
            }, config: default, cancellationToken);
        }
        catch (ReportExceptions exp)
        {
            // TODO:
            // Log here...
            Console.WriteLine(exp.Message);
        }
        return result;
    }
}