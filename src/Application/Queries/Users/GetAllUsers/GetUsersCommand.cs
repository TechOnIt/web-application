using iot.Application.Common.Exceptions;
using iot.Application.Reports.Contracts;
using System.Linq.Expressions;

namespace iot.Application.Queries.Users.GetAllUsers;

public class GetUsersCommand : IRequest<Result<IList<UserViewModel>>>
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, Result<IList<UserViewModel>>>
{
    #region constructor
    private readonly IUserReports _userReports;
    public GetUsersCommandHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<Result<IList<UserViewModel>>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            IList<UserViewModel> users = new List<UserViewModel>();
            Expression<Func<User, bool>> getusersExpression = null;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber) || !string.IsNullOrWhiteSpace(request.Email))
            {
                getusersExpression = a => a.PhoneNumber == request.PhoneNumber || a.Email.Contains(request.Email);
                users = await _userReports.GetByConditionAsync(getusersExpression, null);
            }
            else
            {
                users = await _userReports.GetAllUsersAsync();
            }

            return Result.Ok(users);
        }
        catch (ReportExceptions exp)
        {
            return Result.Fail(exp.Message);
        }
    }
}