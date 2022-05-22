using KeepInformed.Common.DbAccess;
using KeepInformed.Domain.TenantNews.Entities;
using KeepInformed.Infrastructure.BaseDbAccess;
using KeepInformed.Infrastructure.TenantDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.TenantDbAccess;

public class TenantKeepInformedDbContext : BaseDbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<Synchronization> Synchronizations { get; set; }

    public TenantKeepInformedDbContext(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionStringProvider.GetTenantDbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SynchronizationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}