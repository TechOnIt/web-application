using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Common.Models.ViewModels.Roles;
using TechOnIt.Application.Reports.Roles;

namespace TechOnIt.Application.Queries.Roles.GetAllRoles;

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