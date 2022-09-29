using iot.Infrastructure.Persistence.Context.Identity;

namespace iot.Application.Repositories.SQL.LoginHistories;

public sealed class LoginHistoryRepository : ILoginHistoryRepository
{
    #region DI & Ctor
    private readonly IdentityContext _context;
    public LoginHistoryRepository(IdentityContext context)
    {
        _context = context;
    }
    #endregion
}