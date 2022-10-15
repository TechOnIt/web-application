using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels;
using iot.Application.Reports.Contracts;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;

namespace iot.Application.Reports;

public class StructureAggregateReports : IStructureAggregateReports
{
    #region constructor
    private readonly IUnitOfWorks _unitOfWorks;
    public StructureAggregateReports(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }

    #endregion

    public async Task<IList<StructureViewModel>> GetStructuresByFilterAsync(Expression<Func<Structure,bool>> filter,CancellationToken cancellationToken)
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
    
    public async Task<IList<StructureViewModel>> GetstructuresAsync(CancellationToken cancellationToken)
    {
        IList<StructureViewModel> structures = new List<StructureViewModel>();

        try
        {
            var strs= await _unitOfWorks._context.Structures
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

    public IList<StructureViewModel>? GetstructuresSync(CancellationToken cancellationToken)
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
        IList<StructureViewModel> structures = new();

        try
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                degreeOfParallelism = degreeOfParallelism == 0 || degreeOfParallelism > 5 == true ? 3 : degreeOfParallelism;

                structures= _unitOfWorks._context.Users
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