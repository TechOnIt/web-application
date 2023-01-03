using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Common.Extentions;
using TechOnIt.Domain.Entities.Identity.UserAggregate;

namespace TechOnIt.Application.Reports.StructuresAggregate;

public class StructureAggregateReports : IStructureAggregateReports
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public StructureAggregateReports(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion
    public async Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default)
    {
        // Initialize users Query.
        var query = _unitOfWorks._context.Structures.AsQueryable();
        // Apply conditions.
        if (!string.IsNullOrEmpty(paginatedSearch.Keyword))
            query = query
                .Where(u => paginatedSearch.Keyword.Contains(u.Name) ||
                paginatedSearch.Keyword.Contains(u.Description) ||
                paginatedSearch.Keyword.Contains(u.ApiKey.Value))
                .AsQueryable();
        // Execute, pagination and type to project.
        return await query
            .AsNoTracking()
            .PaginatedListAsync<Structure, TDestination>(paginatedSearch.Page, paginatedSearch.PageSize, config, cancellationToken);

    }

    public async Task<IList<StructureViewModel>> GetStructuresByFilterAsync(Expression<Func<Structure, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<Structure> query = _unitOfWorks._context.Structures
                .Where(filter);
            var executeQuery = await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return executeQuery.Adapt<IList<StructureViewModel>>();
        }
        catch (StructureException exp)
        {
            throw new StructureException($"server error : {exp.Message}");
        }
    }

    public async Task<IList<StructureViewModel>> GetstructuresAsync(CancellationToken cancellationToken = default)
    {
        IList<StructureViewModel> structures = new List<StructureViewModel>();

        try
        {
            var strs = await _unitOfWorks._context.Structures
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (strs != null)
                structures = strs.Adapt<IList<StructureViewModel>>();
        }
        catch (StructureException exp)
        {
            throw new StructureException($"server error {exp.Message}");
        }

        return structures;
    }

    public IList<StructureViewModel>? GetstructuresSync(CancellationToken cancellationToken = default)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                return _unitOfWorks._context.Users
                    .AsNoTracking()
                    .ToList()
                    .Adapt<IList<StructureViewModel>>();
            }
            catch (StructureException exp)
            {
                throw new StructureException($"server error : {exp.Message}");
            }
        }

        return null;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="degreeOfParallelism"></param>
    /// <returns></returns>
    /// <exception cref="StructureException"></exception>
    public async Task<IList<StructureViewModel>> GetstructuresParallel(CancellationToken cancellationToken, int degreeOfParallelism = 3)
    {
        IList<StructureViewModel> structures = new List<StructureViewModel>();

        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                degreeOfParallelism = degreeOfParallelism == 0 || degreeOfParallelism > 5 == true ? 3 : degreeOfParallelism;

                structures = _unitOfWorks._context.Users
                    .AsNoTracking()
                    .AsParallel()
                    .WithDegreeOfParallelism(degreeOfParallelism)
                    .Adapt<IList<StructureViewModel>>();
            }
        }
        catch (StructureException exp)
        {
            throw new StructureException($"server error : {exp.Message}");
        }

        return structures;
    }

}