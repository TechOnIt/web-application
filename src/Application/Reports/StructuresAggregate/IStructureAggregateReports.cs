using iot.Application.Common.ViewModels.Structures;
using iot.Domain.Entities.Product.StructureAggregate;
using System.Linq.Expressions;

namespace iot.Application.Reports.StructuresAggregate;

public interface IStructureAggregateReports : IReport
{
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
}