using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Commands.Users.Management.RemoveUserAccount
{
    public class RemoveUserAccountCommand : IRequest<Result>, ICommittableRequest
    {
        public string Id { get; set; }
    }

    public class RemoveUserAccountCommandHandler : IRequestHandler<RemoveUserAccountCommand, Result>
    {
        #region Constructor
        private readonly IUnitOfWorks _unitOfWorks;
        public RemoveUserAccountCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        #endregion

        public async Task<Result> Handle(RemoveUserAccountCommand request, CancellationToken cancellationToken)
        {
            // map id to guid instance.
            var userId = Guid.Parse(request.Id);

            // find user by id.
            var user = await _unitOfWorks.SqlRepository<User>().GetByIdAsync(cancellationToken, userId);

            if (user == null)
                return Result.Fail("User was not found!");

            // delete user & save.
            user.SetIsDelete(true);

            // TODO:
            // Move transaction to pipeline...
            var transAction = await _unitOfWorks._context.Database.BeginTransactionAsync();

            try
            {
                await _unitOfWorks.SqlRepository<User>().UpdateAsync(user, cancellationToken);
            }
            catch (Exception)
            {
                await transAction.RollbackAsync();
                return Result.Fail("An error was occured. try again later."); ;
            }
            return Result.Ok();
        }
    }
}
