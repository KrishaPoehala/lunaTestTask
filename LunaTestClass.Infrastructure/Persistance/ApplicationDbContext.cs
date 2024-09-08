using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LunaTestTask.Infrastructure.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; private set; }
    public DbSet<TaskEntity> Tasks { get; private set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
