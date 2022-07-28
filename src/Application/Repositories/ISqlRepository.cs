using iot.Domain.Common;

namespace iot.Application.Repositories;

public interface ISqlRepository<TEntity> where TEntity : class, IEntity
{
}