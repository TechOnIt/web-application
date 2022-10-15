using System.Linq.Expressions;
using System.Reflection;
using iot.Application.Common.ViewModels;
using iot.Application.Common.ViewModels.Users;
using iot.Domain.Entities.Identity.UserAggregate;

namespace iot.Application.Reports.Contracts;

public interface IUserReports : IReport
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
    Task<IList<UserViewModel>> GetByConditionAsync(Expression<Func<User, bool>> filter = null,
        Func<IQueryable, IOrderedQueryable<User>> orderBy = null,
    params Expression<Func<User, object>>[] includes);

    Task<IList<UserViewModel>> GetUsersInRoleAsync(string roleName, Guid? roleId = null);
    Task<IList<StructureViewModel>?> GetUserStructuresByUserIdAsync(Guid userId);
    Task<IList<DeviceViewModel>?> GetAllDevicesByUserIdAsync(Guid userId);
    PropertyInfo GetUserProperty(string propertyName);
    Task<IList<UserViewModel>> GetAllUsersAsync();
    IList<UserViewModel> GetAllUsersSync(); 
    Task<IList<UserViewModel>> GetAllUsersParallel();
}