using System.Linq.Expressions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Domain.Entities.Catalogs;

namespace TechOnIt.Application.Reports.StructuresAggregate;

public class StructureAggregateReports
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

    public async Task<IList<StructureViewModel>> GetStructuresByFilterAsync(Expression<Func<StructureEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        IQueryable<StructureEntity> query = _unitOfWorks._context.Structures;
        List<StructureEntity> result = await query.AsNoTracking().Where(filter).ToListAsync(cancellationToken);

        return result.Adapt<IList<StructureViewModel>>();
    }

    public async Task<IList<StructureViewModel>> GetstructuresAsync(CancellationToken cancellationToken = default)
    {
        var strs = await _unitOfWorks._context.Structures.AsNoTracking().ToListAsync(cancellationToken);
        return strs.Adapt<IList<StructureViewModel>>();
    }

    public async Task<IList<StructureViewModel>> GetstructuresParallel(CancellationToken cancellationToken)
    {
        var structures = _unitOfWorks._context.Users
            .AsNoTracking()
            .AsParallel()
            .Adapt<IList<StructureViewModel>>();

        return await Task.FromResult(structures);
    }

    public async Task<StructureGroupsWithRelayViewModel?> GetStructureWithGroupsAndRelaysByIdNoTrackAsync(Guid structureId, CancellationToken cancellationToken)
        => await _unitOfWorks._context.Structures
        .AsNoTracking()
        .Include(s => s.Groups)
        .ThenInclude(p => p.Relays)
        .Where(s => s.Id == structureId)
        .ProjectToType<StructureGroupsWithRelayViewModel>()
        .FirstOrDefaultAsync(cancellationToken);
}