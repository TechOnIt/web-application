namespace TechOnIt.Application.Commands.Users.Management.RemoveUserAccount
{
    public class RemoveUserAccountCommand : IRequest<object>, ICommittableRequest
    {
        public Guid UserId { get; set; }
    }

    public class RemoveUserAccountCommandHandler : IRequestHandler<RemoveUserAccountCommand, object>
    {
        #region Constructor
        private readonly IUnitOfWorks _unitOfWorks;
        public RemoveUserAccountCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        #endregion

        public async Task<object> Handle(RemoveUserAccountCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _unitOfWorks.UserRepository.FindByIdAsync(request.UserId,cancellationToken);

                if (user == null)
                    return ResultExtention.NotFound("User was not found!");

                user.Delete();
                await _unitOfWorks.UserRepository.UpdateAsync(user, cancellationToken);

                return ResultExtention.BooleanResult(true);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}
