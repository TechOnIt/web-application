using iot.Domain.Common;

namespace iot.Application.Repositories;

public class SqlRepository<TEntity> : ISqlRepository<TEntity>
        where TEntity : class, IEntity
{
}