using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context;

namespace iot.Infrastructure.UnitOfWorks;

public class UnitOfWork : IUnitOfWorks
{
    #region constructor
    public IdentityContext _context { get; }
    public UnitOfWork(IdentityContext context)
    {
        _context = context;
    }
    #endregion

    #region sql_Generic_Repository
    public ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class
    {
        ISqlRepository<TEntity> repository = new SqlRepository<TEntity, IdentityContext>(_context);
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
    #endregion

    #region EntityFramWork_Methods
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    #endregion
}
