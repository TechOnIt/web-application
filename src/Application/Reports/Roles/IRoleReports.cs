namespace TechOnIt.Application.Reports.Roles;

public interface IRoleReports : IReport
{
    /// <summary>
    /// Find a specific role by id.
    /// </summary>
    /// <param name="roleId">Role unique id.</param>
    Task<Role?> FindByIdAsync(Guid roleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all roles by search and pagination.
    /// </summary>
    /// <typeparam name="TDestination">View model of destination.</typeparam>
    /// <param name="paginatedSearch">Paging info and keyword for search on role name.</param>
    Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default);
}