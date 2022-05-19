using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess;

public class KeepInformedContext : DbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSignedUpConfirmation> UserSignedUpConfirmations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("KeepInformed");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserSignedUpConfirmationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
