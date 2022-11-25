namespace iot.Application.Common.Extentions;

public static class MappingExtensions
{
    public static Task<PaginatedList<TOrigin>> PaginatedListAsync<TOrigin>(this IQueryable<TOrigin> queryable,
        int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        => PaginatedList<TOrigin>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);

    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TOrigin, TDestination>(this IQueryable<TOrigin> queryable,
        int pageNumber, int pageSize, TypeAdapterConfig? config = default, CancellationToken cancellationToken = default)
        => PaginatedList<TDestination>.CreateAsync<TOrigin, TDestination>(queryable, pageNumber, pageSize,
            config, cancellationToken);
}