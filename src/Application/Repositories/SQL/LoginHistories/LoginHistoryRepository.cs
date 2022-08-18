using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.SQL.LoginHistories;

public sealed class LoginHistoryRepository : ILoginHistoryRepository
{
    #region DI & Ctor
    private readonly IIdentityContext _context;
    public LoginHistoryRepository(IIdentityContext context)
    {
        _context = context;
    }
    #endregion
}