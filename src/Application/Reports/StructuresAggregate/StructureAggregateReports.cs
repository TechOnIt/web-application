using TechOnIt.Domain.Entities.Product.StructureAggregate;
using TechOnIt.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;

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

    public async Task<IList<StructureCardViewModel>> GetStructureCardByUserIdNoTrackAsync(Guid userId, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Structures
        .AsNoTracking()
        .Where(s => s.UserId == userId)
        .ProjectToType<StructureCardViewModel>()
        .ToListAsync(cancellationToken);

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

    #region Places
    public async Task<StructurePlacesWithDevicesViewModel> GetStructureWithPlacesAndDevicesByIdNoTrackAsync(Guid structureId, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Structures
        .AsNoTracking()
        .Include(s => s.Places)
        .ThenInclude(p => p.Devices)
        .Where(s => s.Id == structureId)
        .ProjectToType<StructurePlacesWithDevicesViewModel>()
        .FirstOrDefaultAsync(cancellationToken);
    #endregion
}