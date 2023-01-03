using TechOnIt.Application.Common.Extentions;
using TechOnIt.Application.Common.Models;

namespace TechOnIt.Application.Reports.Roles;

public class RoleReports : IRoleReports 
{
    #region Ctors
    private readonly IUnitOfWorks _uow;

    public RoleReports(IUnitOfWorks unitOfWorks)
    {
        _uow = unitOfWorks;
    }
    #endregion

    /// <summary>
    /// Find a specific role by id.
    /// </summary>
    /// <param name="roleId">Role unique id.</param>
    public async Task<Role?> FindByIdAsync(Guid roleId,
        CancellationToken cancellationToken = default)
        => await _uow._context.Roles.FindAsync(roleId, cancellationToken);

    /// <summary>
    /// Get all roles by search and pagination.
    /// </summary>
    /// <typeparam name="TDestination">View model of destination.</typeparam>
    /// <param name="paginatedSearch">Paging info and keyword for search on role name.</param>
    public async Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default)
    {
        var query = _uow._context.Roles.AsQueryable();
        #region Search condition
        if (!string.IsNullOrEmpty(paginatedSearch.Keyword))
            query = query
                .Where(role => role.Name.Contains(paginatedSearch.Keyword))
                .AsQueryable();
        #endregion
        return await query.PaginatedListAsync<Role, TDestination>(paginatedSearch.Page, paginatedSearch.PageSize, config, cancellationToken);
    }
}