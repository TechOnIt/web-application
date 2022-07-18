using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Infrastructure.Persistence.Context;

public class IdentityContext : DbContext
{
    public DbSet<User> Users { get; set; }
}