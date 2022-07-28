using iot.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace iot.Application.Common.Interfaces.Context;

public interface IIdentityContext
{
    public DbSet<User> Users { get; set; }
}