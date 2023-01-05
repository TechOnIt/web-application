using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.GetAllUsers;

public class GetUsersQuery : PaginatedSearch, IRequest<PaginatedList<UserViewModel>>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserViewModel>>
{
    #region constructor
    private readonly IUserReports _userReports;
    public GetUsersQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<PaginatedList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken = default)
    {
        var result = new PaginatedList<UserViewModel>();
        try
        {
            result = await _userReports.GetAllPaginatedSearchAsync<UserViewModel>(new PaginatedSearchWithSize
            {
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = 20
            },
            config: UserViewModel.Config(), cancellationToken);
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