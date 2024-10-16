using TechOnIt.Application.Common.Models.ViewModels.Roles;
using TechOnIt.Application.Reports.Roles;

namespace TechOnIt.Application.Queries.Roles.GetAllRoles;

public class GetRolesQuery : PaginatedSearchWithSize, IRequest<PaginatedList<RoleWithUsersCountViewModel>>
{
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleWithUsersCountViewModel>>
{
    #region Ctor
    private readonly RoleReports _roleReports;

    public GetRolesQueryHandler(RoleReports roleReports)
    {
        _roleReports = roleReports;
    }
    #endregion

    public async Task<PaginatedList<RoleWithUsersCountViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken = default)
    {
        var config = TypeAdapterConfig<RoleEntity, RoleWithUsersCountViewModel>.NewConfig()
            .Map(dest => dest.UsersCount, src => src.UserRoles.Count)
            .Config
            ;
        PaginatedList<RoleWithUsersCountViewModel> result = new();
        try
        {
            result = await _roleReports.GetAllPaginatedSearchAsync<RoleWithUsersCountViewModel>(new PaginatedSearchWithSize
            {
                Keyword = request.Keyword,
                Page = request.Page,
                PageSize = request.PageSize
            }, config: config, cancellationToken);
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