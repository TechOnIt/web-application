using System.Linq.Expressions;
using TechOnIt.Domain.Entities.Catalogs;
using TechOnIt.Domain.ValueObjects;

namespace TechOnIt.Application.Common.Interfaces.Repositories;

public interface IStructureRepository
{
    #region Structure
    Task<StructureEntity> FindByApiKeyAndPasswordAsync(Concurrency apiKey, PasswordHash password, CancellationToken cancellationToken);
    Task CreateAsync(StructureEntity structure, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(StructureEntity structure, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid structureId, CancellationToken cancellationToken);
    Task<StructureEntity> GetByIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<StructureEntity> GetByIdAsyncAsNoTracking(Guid structureId, CancellationToken cancellationToken = default);
    Task<IList<StructureEntity>> GetAllByFilterAsync(CancellationToken cancellationToken, Expression<Func<StructureEntity, bool>> filter = null);
    #endregion

    #region Common
    Task CreateWithGroupsAsync(StructureEntity structure, IList<GroupEntity> groups, CancellationToken cancellationToken = default);
    Task<IList<GroupEntity>> GetGroupsByStructureIdAsync(Guid structureId, CancellationToken cancellationToken = default);
    Task<GroupEntity> GetGroupByStructureIdAsync(Guid structorId, Guid groupId, CancellationToken cancellationToken);
    #endregion

    #region Group
    Task<GroupEntity> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken = default);
    Task<GroupEntity> GetGroupByIdAsyncAsNoTracking(Guid groupId, CancellationToken cancellationToken = default);
    Task<IList<GroupEntity>> GetAllGroupsByFilterAsync(CancellationToken cancellationToken, Expression<Func<GroupEntity, bool>> filter = null);
    Task CreateGroupAsync(GroupEntity group, Guid StructureId, CancellationToken cancellationToken = default);
    Task UpdateGroupAsync(Guid structureId, GroupEntity group, CancellationToken cancellationToken = default);
    Task<bool> DeleteGroupAsync(Guid groupId, Guid structureId, CancellationToken cancellationToken);
    #endregion
}
