using iot.Application.Common.Interfaces.Context;
using iot.Domain.Entities.Identity;

namespace iot.Application.Repositories.SQL.LoginHistories
{
    internal sealed class LoginHistoryRepository : SqlRepository<LoginHistory>, ILoginHistoryRepository
    {
        public LoginHistoryRepository(IIdentityContext context)
            : base(context) { }
    }
}