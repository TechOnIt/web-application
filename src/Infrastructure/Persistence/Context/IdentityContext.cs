using iot.Application.Common.Interfaces.Context;
using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Infrastructure.Persistence.Context;

public class IdentityContext : DbContext, IIdentityContext
{
    public DbSet<User> Users { get; set; }
}