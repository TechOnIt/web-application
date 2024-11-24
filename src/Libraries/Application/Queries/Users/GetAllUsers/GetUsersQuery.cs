using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;
using TechOnIt.Domain.Entities.Identities.UserAggregate;

namespace TechOnIt.Application.Queries.Users.GetAllUsers;

public class GetUsersQuery : PaginatedSearch, IRequest<PaginatedList<UserViewModel>>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserViewModel>>
{
    #region Ctor
    private readonly UserReports _userReports;
    public GetUsersQueryHandler(UserReports userReports)
    {
        _userReports = userReports;
    }
    #endregion

    public async Task<PaginatedList<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken = default)
    {
        var result = new PaginatedList<UserViewModel>();
        try
        {
            #region Map Config
            var mapConfig = TypeAdapterConfig<UserEntity, UserViewModel>.NewConfig()
            .Map(dest => dest.ConcurrencyStamp, src => src.ConcurrencyStamp)
            .Map(dest => dest.RegisteredDateTime, src => src.RegisteredAt.ToString("yyyy/MM/dd HH:mm:ss")).Config;
            #endregion

            result = await _userReports.GetAllPaginatedSearchAsync<UserViewModel>(new PaginatedSearchWithSize
            {
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = 20
            },
            config: mapConfig, cancellationToken);
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