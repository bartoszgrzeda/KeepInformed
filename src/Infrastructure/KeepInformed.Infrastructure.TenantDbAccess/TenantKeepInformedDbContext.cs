using KeepInformed.Common.DbAccess;
using KeepInformed.Infrastructure.BaseDbAccess;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.TenantDbAccess;

public class TenantKeepInformedDbContext : BaseDbContext
{
    public TenantKeepInformedDbContext(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionStringProvider.GetTenantDbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}