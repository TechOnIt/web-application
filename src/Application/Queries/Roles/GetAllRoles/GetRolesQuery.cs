using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels.Roles;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Reports.Roles;
using iot.Application.Reports.Users;

namespace iot.Application.Queries.Roles.GetAllRoles;

public class GetRolesQuery : PaginatedSearchWithSize, IRequest<PaginatedList<RoleViewModel>>
{
}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleViewModel>>
{
    #region Ctor
    private readonly IRoleReports _roleReports;

    public GetRolesQueryHandler(IRoleReports roleReports)
    {
        _roleReports = roleReports;
    }
    #endregion

    public async Task<PaginatedList<RoleViewModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken = default)
    {
        PaginatedList<RoleViewModel> result = new();
        try
        {
            result = await _roleReports.GetAllPaginatedSearchAsync<RoleViewModel>(new PaginatedSearchWithSize
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