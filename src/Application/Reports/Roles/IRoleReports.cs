using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Reports;

namespace TechOnIt.Application.Reports.Roles;

public interface IRoleReports : IReport
{
    Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default);
}