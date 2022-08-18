using iot.Application.Queries.Users.GetAllUsers.Commands;
using iot.Application.Repositories.SQL.Users;
using Mapster;

namespace iot.Application.Queries.Users.GetAllUsers.Handlers
{
    public class AllUsersCommandHandler : IRequestHandler<AllUsersCommand, IList<UserViewModel>>
    {
        #region constructor
        private readonly IUnitOfWorks _unitOfWork;

        public AllUsersCommandHandler(IUnitOfWorks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<IList<UserViewModel>> Handle(AllUsersCommand request, CancellationToken cancellationToken)
        {
            IList<UserViewModel> viewModel = new List<UserViewModel>();
            var getUsers = await _unitOfWork.UserRepository.GetAllUsersByFilterAsync(cancellationToken: cancellationToken);

            if (getUsers != null)
                viewModel = getUsers.Adapt<IList<UserViewModel>>();

            return viewModel;
        }
    }
}
