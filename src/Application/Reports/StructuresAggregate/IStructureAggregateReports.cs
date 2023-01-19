using TechOnIt.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;
using TechOnIt.Application.Common.Exceptions;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Reports;

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
    /// use when you have less than 200 structures in database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="StructureException"></exception>
    IList<StructureViewModel>? GetstructuresSync(CancellationToken cancellationToken = default);

    /// <summary>
    /// only when you have more than 1000 structures in database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="degreeOfParallelism"></param>
    /// <returns></returns>
    /// <exception cref="StructureException"></exception>
    Task<IList<StructureViewModel>> GetstructuresParallel(CancellationToken cancellationToken, int degreeOfParallelism = 3);

    #region Place
    Task<StructurePlacesWithDevicesViewModel?> GetStructureWithPlacesAndDevicesByIdNoTrackAsync(Guid structureId, CancellationToken cancellationToken);
    #endregion
}