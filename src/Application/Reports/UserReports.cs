using iot.Application.Common.Exceptions;
using iot.Application.Common.ViewModels;
using iot.Application.Common.ViewModels.Users;
using iot.Application.Reports.Contracts;
using iot.Domain.Entities.Identity.UserAggregate;
using iot.Domain.Entities.Product.StructureAggregate;
using iot.Infrastructure.Repositories.UnitOfWorks;
using Mapster;
using System.Linq.Expressions;
using System.Reflection;

namespace iot.Application.Reports;

public class UserReports : IUserReports
{
	#region constructor
	private readonly IUnitOfWorks _unitOfWorks;
	public UserReports(IUnitOfWorks unitOfWorks)
	{
		_unitOfWorks = unitOfWorks;
	}
    #endregion

    /// <summary>
    /// try to do not use of this method when you have less than 1000 users in database for better perfomance
    /// </summary>
    /// <returns>IList<UserViewModel></returns>
    public async Task<IList<UserViewModel>> GetAllUsersParallel()
        => _unitOfWorks._context.Users.AsNoTracking()
        .AsParallel()
        .WithDegreeOfParallelism(3)
        .Adapt<IList<UserViewModel>>();

    /// <summary>
    /// use this method when you have less than 200 users in database
    /// </summary>
    /// <returns>IList<UserViewModel></returns>
    public IList<UserViewModel> GetAllUsersSync()
        => _unitOfWorks._context.Users.AsNoTracking().ToList().Adapt<IList<UserViewModel>>();

    public async Task<IList<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _unitOfWorks._context.Users.AsNoTracking().ToListAsync();
        return users.Adapt<IList<UserViewModel>>();
    }

    public async Task<IList<UserViewModel>> GetByConditionAsync(Expression<Func<User,bool>> filter = null,
        Func<IQueryable, IOrderedQueryable<User>> orderBy = null,
    params Expression<Func<User, object>>[] includes)
    {
        IQueryable<User> query = _unitOfWorks._context.Users;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var executionQueries = await query.AsNoTracking().ToListAsync();
        return executionQueries.Adapt<IList<UserViewModel>>();
    }
    public async Task<IList<UserViewModel>> GetUsersInRoleAsync(string roleName,Guid? roleId=null)
    {
        IQueryable<User> query = null;
        IList<User> users= new List<User>();

        if (roleId == null)
        {
            var getRole = await _unitOfWorks._context.Roles.AsNoTracking().FirstOrDefaultAsync(a=>a.Name==roleName);
            if (getRole == null)
                return new List<UserViewModel>();

            Guid[] allUsersInRole = await _unitOfWorks._context.UserRoles.AsNoTracking().Where(a => a.RoleId == getRole.Id)
                .Select(a => a.UserId).ToArrayAsync();

            if (allUsersInRole.Length > 0)
            {
                query = _unitOfWorks._context.Users.AsNoTracking()
                   .Where(a => (allUsersInRole.Any(x => x == a.Id)) == true);

                users = await query.ToListAsync();
            }
        }
        else
        {
            Guid[] usersInRole = await _unitOfWorks._context.UserRoles.AsNoTracking().Where(a => a.RoleId == roleId)
                .Select(a => a.UserId).ToArrayAsync();

            if (usersInRole.Length > 0)
            {
                 query = _unitOfWorks._context.Users.AsNoTracking()
                    .Where(a => (usersInRole.Any(x => x == a.Id)) == true);

                users = await query.ToListAsync();
            }
        }

        return users.Adapt<IList<UserViewModel>>();
    }
    public async Task<IList<StructureViewModel>?> GetUserStructuresByUserIdAsync(Guid userId)
    {
        try
        {
            var user = await _unitOfWorks.UserRepository.FindUserByUserIdAsNoTrackingAsync(userId);
            if (user == null)
                return null;

            Expression<Func<Structure, bool>> getStructures = a => a.UserId == user.Id;
            var cancellationSource = new CancellationToken();

            var structures = await _unitOfWorks.StructureRepository.GetAllStructuresByFilterAsync(cancellationSource,getStructures);
            if (structures is null)
                return null;

            return structures.Adapt<IList<StructureViewModel>>();
        }
        catch
        {
            return null;
        }
    }
    public async Task<IList<DeviceViewModel>?> GetAllDevicesByUserIdAsync(Guid userId)
    {
        try
        {
            var devices = await (from str in _unitOfWorks._context.Structures

                                 join plc in _unitOfWorks._context.Places on str.Id equals plc.StuctureId
                                 into place
                                 from pl in place.DefaultIfEmpty()

                                 join dev in _unitOfWorks._context.Devices on pl.Id equals dev.PlaceId
                                 into device
                                 from de in device.DefaultIfEmpty()

                                 where str.UserId == userId

                                 select new DeviceViewModel
                                 {
                                     Id=de.Id,
                                     Pin=de.Pin,
                                     DeviceType=de.DeviceType,
                                     IsHigh=de.IsHigh,
                                     PlaceId=de.PlaceId,

                                 }).AsNoTracking().ToListAsync();

            return devices;
        }
        catch (ReportExceptions exp)
        {
            throw new ReportExceptions($"error in geting all user devices by user Id : {exp.UserId}");
        }
    }

    public PropertyInfo? GetUserProperty(string propertyName)
    {
        // get all public static properties of MyClass type
        PropertyInfo[] propertyInfos;
        propertyInfos = typeof(User)
            .GetProperties(BindingFlags.Public | BindingFlags.Static);

        // sort properties by name
        Array.Sort(propertyInfos,
                delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

        PropertyInfo? result = null;

        // write property names
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (propertyInfo.Name.ToLower().Contains(propertyName.ToLower()))
                result = propertyInfo;

            if (!string.IsNullOrWhiteSpace(result.Name))
                break;
        }


        return result;
    }
}