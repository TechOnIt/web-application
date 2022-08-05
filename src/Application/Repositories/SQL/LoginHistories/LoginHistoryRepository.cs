using iot.Application.Common.Interfaces.Context;
using iot.Application.Common.Interfaces.Dependency;

namespace iot.Application.Repositories.SQL.LoginHistories
{
    internal sealed class LoginHistoryRepository : SqlRepository<LoginHistory>, ILoginHistoryRepository, IScopedDependency
    {
        public LoginHistoryRepository(IIdentityContext context)
            : base(context) { }
    }
}