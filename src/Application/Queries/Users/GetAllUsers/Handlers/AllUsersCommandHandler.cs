using iot.Application.Commands.Users.Queries.Commands;
using iot.Application.Repositories.SQL.Users;
using Mapster;

namespace iot.Application.Commands.Users.Queries.Handlers
{
    public class AllUsersCommandHandler : IRequestHandler<AllUsersCommand, IList<UserViewModel>>
    {
        #region constructor
        private readonly IUserRepository _userRepository;
        public AllUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public async Task<IList<UserViewModel>> Handle(AllUsersCommand request, CancellationToken cancellationToken)
        {
            IList<UserViewModel> viewModel = new List<UserViewModel>();
            var getUsers = await _userRepository.GetAllUsersAsync(request.ExpressionCondition);

            if (getUsers != null)
                viewModel = getUsers.Adapt<IList<UserViewModel>>();

            return viewModel;
        }
    }
}
