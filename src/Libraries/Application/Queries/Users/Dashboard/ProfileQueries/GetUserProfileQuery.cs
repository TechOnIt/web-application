using TechOnIt.Application.Common.Models.ViewModels.Users;
using TechOnIt.Application.Reports.Users;

namespace TechOnIt.Application.Queries.Users.Dashboard.ProfileQueries;

public class GetUserProfileQuery : IRequest<UserViewModel?>
{
    public Guid UserId { get; set; }
}

public sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserViewModel?>
{
    private readonly IUserReports _userReports;
    public GetUserProfileQueryHandler(IUserReports userReports)
    {
        _userReports = userReports;
    }

    public async Task<UserViewModel?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        return await _userReports.FindByIdNoTrackAsViewModelAsync(request.UserId, cancellationToken);
    }
}

public sealed class GetUserProfileQueryValidator : BaseFluentValidator<GetUserProfileQuery>
{
    public GetUserProfileQueryValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            ;
    }
}