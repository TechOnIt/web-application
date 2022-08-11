﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users.Management.SetUserPassword
{
    public class SetUserPasswordCommand : IRequest<Result> , ICommittableRequest
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }

    public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, Result>
    {
        #region Constructor
        private readonly IUnitOfWorks _unitOfWorks;
        public SetUserPasswordCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        #endregion


        public async Task<Result> Handle(SetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            // map id to guid i;nstance.
            var userId = Guid.Parse(request.Id);

            // find user by id.
            var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);


            if (user == null)
                return Result.Fail("User was not found!");

            // Set new password.
            user.SetPassword(PasswordHash.Parse(request.Password));

            var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();
            try
            {
                // Update user.
                bool saveWasSuccess = await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, saveNow: true, cancellationToken);

                if (saveWasSuccess == false)
                    return Result.Fail("An error was occured. try again later.");

                await transAction.CommitAsync();
            }
            catch
            {
                await transAction.RollbackAsync();
                return Result.Fail("An error was occured. try again later.");
            }

            return Result.Ok();
        }
    }
}
