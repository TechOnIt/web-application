using iot.Application.Common.Models;
using iot.Application.Repositories.SQL.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace iot.Application.Queries.Users.FindById;

public class FindUserByIdQuery : Query<Result<UserViewModel>>
{
    // input parameter
    public Guid Id { get; set; }
}

public class FindByIdQueryHandler : QueryHandler<FindUserByIdQuery, Result<UserViewModel>>
{
    #region DI & Ctor's
    private readonly IUserRepository _userRepository;
    public FindByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion

    public async Task<Result<UserViewModel>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Find user by id.
        var user = await _userRepository.TableNoTracking
            .FirstOrDefaultAsync(u => u.Id == request.Id);
        // User not found.
        if (user == null)
        {
            return Result.Fail("کاربر پیدا نشد.");
        }
        // Map user to view model & return.
        return user.Adapt<UserViewModel>();
    }
}