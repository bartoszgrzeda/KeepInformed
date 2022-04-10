using KeepInformed.Domain.Tvn.Entities;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations;
using KeepInformed.Infrastructure.DbAccess.EntityConfigurations.Tvn;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Context;

public class TvnContext : DbContext
{
    public DbSet<TvnNews> TvnNews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Tvn");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TvnNewsEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
