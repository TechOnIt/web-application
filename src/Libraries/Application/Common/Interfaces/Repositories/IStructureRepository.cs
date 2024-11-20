using System.Linq.Expressions;
using TechOnIt.Domain.Entities.Catalog;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IStructureRepository
{
    #region Structure
    Task<Structure> FindByApiKeyAndPasswordAsync(Concurrency apiKey, PasswordHash password, CancellationToken cancellationToken);
    Task CreateAsync(Structure structure, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Structure structure, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken);
    Task<Structure> GetByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Structure> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<Structure>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<Structure, bool>> filter = null);
    #endregion

    #region Common
    Task CreateWithGroupsAsync(Structure structure, IList<Group> groups, CancellationToken cancellationToken = default);
    Task<IList<Group>> GetGroupsByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<Group> GetGroupByStructureIdAsync(Guid structorId, Guid groupId, CancellationToken cancellationToken);
    #endregion

    #region Group
    Task<Group> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<Group> GetGroupByIdAsyncAsNoTracking(Guid groupId, CancellationToken cancellationToken = default);
    Task<IList<Group>> GetAllGroupsByFilterAsync(CancellationToken cancellationToken, Expression<Func<Group, bool>> filter = null);
    Task CreateGroupAsync(Group group, Guid StructureId, CancellationToken cancellationToken = default);
    Task UpdateGroupAsync(Guid structureId, Group group, CancellationToken cancellationToken = default);
    Task<bool> DeleteGroupAsync(Guid groupId, Guid structureId, CancellationToken cancellationToken);
    #endregion
}
