using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess;

public class KeepInformedContext : DbContext
{
    public DbSet<News> News { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("KeepInformed");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
