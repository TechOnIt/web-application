using iot.Infrastructure.Persistence.Context;

namespace iot.Application.Repositories.SQL.LoginHistories
{
    public sealed class LoginHistoryRepository : SqlRepository<LoginHistory>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(IdentityContext context)
            : base(context) { }
    }
}