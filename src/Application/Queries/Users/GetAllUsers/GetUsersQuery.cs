using iot.Application.Common.Exceptions;
using iot.Application.Common.Models.ViewModels.Users;
using iot.Application.Reports.Users;

namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersQuery : Paginated, IRequest<PaginatedList<UserViewModel>>
{
    public string? Keyword { get; set; }
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
            config: default, cancellationToken);
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