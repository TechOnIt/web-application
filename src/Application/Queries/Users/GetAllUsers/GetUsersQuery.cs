using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Reports.Contracts;

namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersQuery : Paginated, IRequest<Result<PaginatedList<UserViewModel>>>
{
    public string? Keyword { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PaginatedList<UserViewModel>>>
{
    #region constructor
    private readonly IUserReports _userReports;
    public GetUsersQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<Result<PaginatedList<UserViewModel>>> Handle(GetUsersQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var paginatedUsers = await _userReports.GetByQueryAndPaginationAndMapAsync<UserViewModel>(request.Keyword, request.Page, 20, default, cancellationToken);
            return Result.Ok(paginatedUsers);
        }
        catch (ReportExceptions exp)
        {
            return Result.Fail(exp.Message);
        }
    }
}