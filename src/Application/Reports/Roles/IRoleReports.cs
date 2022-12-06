namespace iot.Application.Reports.Roles;

public interface IRoleReports : IReport
{
    Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default);
}