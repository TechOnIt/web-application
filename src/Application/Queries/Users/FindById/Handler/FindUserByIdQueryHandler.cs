using iot.Application.Queries.Users.FindById.Command;
using iot.Application.Repositories.UnitOfWorks.Identity;
using Mapster;
using System;
using System.Collections.Generic;

namespace iot.Application.Queries.Users.FindById.Handler;

public class FindByIdQueryHandler : IRequestHandler<FindUserByIdQuery, Result<UserViewModel>>
{
    #region DI & Ctor's
    private readonly IUnitOfWorks _unitOfWorks;
    public FindByIdQueryHandler(IUnitOfWorks unitOfWorks)
    {
        _unitOfWorks = unitOfWorks;
    }
    #endregion

    public async Task<Result<UserViewModel>> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Find user by id.
        var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken,request.Id);

        // User not found.
        if (user == null) return Result.Fail("کاربر پیدا نشد.");

        // Map user to view model & return.
        return user.Adapt<UserViewModel>();
    }
}
