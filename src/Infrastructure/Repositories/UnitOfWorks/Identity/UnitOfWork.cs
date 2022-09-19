using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Roles;
using iot.Application.Repositories.SQL.StructureAggregateRepository;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.UnitOfWorks.Identity;

public class UnitOfWork : IUnitOfWorks
{
    #region constructor
    public IIdentityContext _context { get; }

    public UnitOfWork(IIdentityContext context)
    {
        _context = context;
    }
    #endregion

    #region sql_Generic_Repository
    public ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class
    {
        ISqlRepository<TEntity> repository = new SqlRepository<TEntity, IIdentityContext>(_context);
        return repository;
    }
    #endregion

    #region categoryRepository
    private IUserRepository _userRepository;
    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new UserRepository(_context);
            }
            return _userRepository;
        }
    }

    private IRoleRepository _roleRepository;
    public IRoleRepository RoleRepository
    {
        get
        {
            if (_roleRepository == null)
            {
                _roleRepository = new RoleRepository(_context);
            }
            return _roleRepository;
        }
    }
    #endregion

    #region StructureRepository
    private IStructureRepository _structureRepository;
    public IStructureRepository StructureRepository
    {
        get
        {
            if(_structureRepository is null)
            {
                _structureRepository = new StructureRepository(_context);
            }

            return _structureRepository;
        }
    }
    #endregion

    #region EF Methods
    public async Task SaveAsync(CancellationToken stoppingToken = default)
    {
        await _context.SaveChangesAsync(stoppingToken);
    }
    #endregion
}
