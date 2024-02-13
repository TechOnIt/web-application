namespace TechOnIt.Application.Reports.Roles;

public class RoleReports
{
    #region Ctors
    private readonly IUnitOfWorks _uow;

    public RoleReports(IUnitOfWorks unitOfWorks)
    {
        _uow = unitOfWorks;
    }
    #endregion

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
        return await query.PaginatedListAsync<RoleEntity, TDestination>(paginatedSearch.Page, paginatedSearch.PageSize, config, cancellationToken);
    }
}