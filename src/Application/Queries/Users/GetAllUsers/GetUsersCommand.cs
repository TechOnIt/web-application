using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Reports.Contracts;

namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersCommand : Paginated, IRequest<Result<PaginatedList<UserViewModel>>>
{
    public string? Keyword { get; set; }
}

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, Result<PaginatedList<UserViewModel>>>
{
    #region constructor
    private readonly IUserReports _userReports;
    public GetUsersCommandHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<Result<PaginatedList<UserViewModel>>> Handle(GetUsersCommand request, CancellationToken cancellationToken = default)
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