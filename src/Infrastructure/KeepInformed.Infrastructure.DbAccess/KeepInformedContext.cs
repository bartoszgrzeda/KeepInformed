using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess;

public class KeepInformedContext : DbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmailConfirmation> UserEmailConfirmations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=KeepInformed;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailConfirmationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
