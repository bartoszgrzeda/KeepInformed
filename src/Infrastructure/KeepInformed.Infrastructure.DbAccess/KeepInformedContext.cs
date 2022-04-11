using KeepInformed.Domain.Tvn.Entities;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations.Tvn;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess;

public class KeepInformedContext : DbContext
{
    public DbSet<TvnNews> TvnNews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("KeepInformed");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TvnNewsEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
