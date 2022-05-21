using KeepInformed.Common.DbAccess;
using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.MasterNews.Entities.Tvn;
using KeepInformed.Infrastructure.BaseDbAccess;
using KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations;
using KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations.Tvn;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.MasterDbAccess;

public class MasterKeepInformedDbContext : BaseDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmailConfirmation> UserEmailConfirmations { get; set; }

    public DbSet<TvnNews> TvnNews { get; set; }

    public MasterKeepInformedDbContext(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionStringProvider.GetMasterDbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailConfirmationEntityConfiguration());

        modelBuilder.ApplyConfiguration(new TvnNewsEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
