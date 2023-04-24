using TechOnIt.Domain.Entities.Identity.UserAggregate;
using System.Linq.Expressions;
using System.Reflection;
using TechOnIt.Application.Common.Models;
using TechOnIt.Application.Common.Models.ViewModels.Devices;
using TechOnIt.Application.Common.Models.ViewModels.Structures;
using TechOnIt.Application.Common.Models.ViewModels.Users;

namespace TechOnIt.Application.Reports.Users;

public interface IUserReports : IReport
{
    Task<UserViewModel?> FindByIdAsViewModelAsync(Guid id, CancellationToken cancellationToken);
    Task<UserViewModel?> FindByIdNoTrackAsViewModelAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Get specific user information by userame.
    /// </summary>
    /// <param name="username">unique username</param>
    Task<UserViewModel?> FindByUsernameNoTrackAsViewModelAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Find an specific user by Email, phone number, username.
    /// </summary>
    /// <param name="identity">Email, Phone Number, Username</param>
    Task<User?> FindByIdentityNoTrackAsync(string identity, CancellationToken cancellationToken);

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

    /// <summary>
    /// Apply condition, specify the viewmodel and pagination.
    /// </summary>
    /// <typeparam name="TDestination">Type of view model.</typeparam>
    /// <param name="predicate">Condition in 'WHERE'.</param>
    /// <param name="page">Page index. (defatult = 1)</param>
    /// <param name="pageSize">Page items scope.</param>
    /// <param name="config">Config for mapster.</param>
    /// <returns>The output you specified yourself!</returns>
    Task<PaginatedList<TDestination>> GetAllPaginatedSearchAsync<TDestination>(PaginatedSearchWithSize paginatedSearch,
        TypeAdapterConfig? config = null, CancellationToken cancellationToken = default);

    Task<IList<UserViewModel>> GetUsersInRoleAsync(string roleName, Guid? roleId = null);
    Task<IList<StructureViewModel>?> GetUserStructuresByUserIdAsync(Guid userId);
    Task<IList<DeviceViewModel>?> GetAllDevicesByUserIdAsync(Guid userId);
    PropertyInfo? GetUserProperty(string propertyName);
    Task<IList<UserViewModel>> GetAllUsersAsync();
    IList<UserViewModel> GetAllUsersSync();
    Task<IList<UserViewModel>> GetAllUsersParallel();

    Task<object> GetNewUsersCountGroupbyRegisterDateAsync(DateTime from, CancellationToken cancellationToken);
}