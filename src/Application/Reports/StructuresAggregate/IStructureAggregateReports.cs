using System.Linq.Expressions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Domain.Entities.StructureAggregate;

namespace TechOnIt.Application.Reports.StructuresAggregate;

public interface IStructureAggregateReports : IReport
{
    Task<IList<StructureCardViewModel>> GetStructureCardByUserIdNoTrackAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// filter is a Expression linq
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="StructureException"></exception>
    Task<IList<StructureViewModel>> GetStructuresByFilterAsync(Expression<Func<Structure, bool>> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// use for get structures from databes async
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="StructureException"></exception>
    Task<IList<StructureViewModel>> GetstructuresAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// only when you have more than 1000 structures in database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>IList<StructureViewModel></returns>
    Task<IList<StructureViewModel>> GetstructuresParallel(CancellationToken cancellationToken);

    #region Place
    Task<StructurePlacesWithDevicesViewModel?> GetStructureWithPlacesAndDevicesByIdNoTrackAsync(Guid structureId, CancellationToken cancellationToken);
    #endregion
}