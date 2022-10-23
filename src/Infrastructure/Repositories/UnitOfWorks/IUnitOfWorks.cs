﻿using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.StructureAggregateRepository;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context;
using iot.Infrastructure.Repositories.SQL.Device;
using iot.Infrastructure.Repositories.SQL.SensorAggregate;

namespace iot.Infrastructure.Repositories.UnitOfWorks;

public interface IUnitOfWorks
{
    Task SaveAsync(CancellationToken stoppingToken = default, bool fixArabicChars = false);
    ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
    IdentityContext _context { get; }

    IStructureRepository StructureRepository { get; }
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ISensorRepository SensorRepository { get; }
    IDeviceRepositry DeviceRepositry { get; }
}