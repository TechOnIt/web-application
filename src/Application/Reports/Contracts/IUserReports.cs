using System.Linq.Expressions;
using System.Reflection;

namespace iot.Application.Reports.Contracts;

public interface IUserReports : IReport
{
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
