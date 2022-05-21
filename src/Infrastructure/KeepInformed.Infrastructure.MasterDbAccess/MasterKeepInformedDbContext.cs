using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.BaseDbAccess;
using KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KeepInformed.Infrastructure.MasterDbAccess;

public class MasterKeepInformedDbContext : BaseDbContext
{
    private readonly string _connectionString;

    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmailConfirmation> UserEmailConfirmations { get; set; }

    public MasterKeepInformedDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("ConnectionStrings")["KeepInformed-Db"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=KeepInformed-MasterDb;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailConfirmationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
