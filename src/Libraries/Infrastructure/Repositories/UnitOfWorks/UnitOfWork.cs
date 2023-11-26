using TechOnIt.Infrastructure.Repositories.SQL.Devices;
using TechOnIt.Infrastructure.Repositories.SQL.Roles;
using TechOnIt.Infrastructure.Repositories.SQL.Users;
using System.Reflection;
using TechOnIt.Infrastructure.Persistence.Context;
using TechOnIt.Infrastructure.Repositories.SQL.SensorAggregate;
using TechOnIt.Infrastructure.Repositories.SQL.StructureAggregateRepository;
using TechOnIt.Infrastructure.Repositories.SQL.Reports;
using TechOnIt.Infrastructure.Repositories.SQL.HeavyTransaction;

namespace TechOnIt.Infrastructure.Repositories.UnitOfWorks;

public class UnitOfWork : IUnitOfWorks
{
    #region constructor
    public IdentityContext _context { get; }

    public UnitOfWork(IdentityContext context)
    {
        _context = context;
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
            if (_structureRepository is null)
            {
                _structureRepository = new StructureRepository(_context);
            }

            return _structureRepository;
        }
    }
    #endregion

    #region SensorRepository
    private ISensorRepository _sensorRepository;
    public ISensorRepository SensorRepository
    {
        get
        {
            if (_sensorRepository == null)
                _sensorRepository = new SensorRepository(_context);

            return _sensorRepository;
        }
    }
    #endregion

    #region DeviceRepository
    private IDeviceRepositry _deviceRepositry;
    public IDeviceRepositry DeviceRepositry
    {
        get
        {
            if (_deviceRepositry == null)
            {
                _deviceRepositry = new DeviceRepositry(_context);
            }
            return _deviceRepositry;
        }
    }
    #endregion

    #region Report Repository
    private IReportRepository _reportRepository;
    public IReportRepository ReportRepository
    {
        get
        {
            if (_reportRepository is null)
                _reportRepository = new ReportRepository(_context);

            return _reportRepository;
        }
    }
    #endregion

    #region Ado .net
    private IAdoRepository _adoRepository;
    public IAdoRepository AdoRepository
    {
        get
        {
            if (_adoRepository is null)
                _adoRepository = new AdoRepository();

            return _adoRepository;
        }
    }
    #endregion

    #region EF Methods
    public async Task SaveAsync(CancellationToken stoppingToken = default, bool fixArabicChars = false)
    {
        if (fixArabicChars)
            _cleanString();

        await _context.SaveChangesAsync(stoppingToken);
    }
    #endregion

    #region private methods
    private void _cleanString()
    {
        var changedEntities = _context.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                var val = (string)property.GetValue(item.Entity, null);

                if (!string.IsNullOrWhiteSpace(val))
                {
                    var newVal = val.NumberFa2En().FixPersianChars();
                    if (newVal == val)
                        continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }
    #endregion
}
