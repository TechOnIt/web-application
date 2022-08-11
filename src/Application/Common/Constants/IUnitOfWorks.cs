using iot.Application.Repositories.SQL;
using iot.Application.Repositories.SQL.Users;
using iot.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Application.Common.Constants
{
    public interface IUnitOfWorks
    {
        ISqlRepository<TEntity> SqlRepository<TEntity>() where TEntity : class;
        IUserRepository UserRepository { get; }
        IdentityContext _context { get; }
        Task SaveAsync();
    }
}
