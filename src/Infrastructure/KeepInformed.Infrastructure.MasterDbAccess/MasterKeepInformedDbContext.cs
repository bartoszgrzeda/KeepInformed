﻿using KeepInformed.Common.DbAccess;
using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.BaseDbAccess;
using KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.MasterDbAccess;

public class MasterKeepInformedDbContext : BaseDbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEmailConfirmation> UserEmailConfirmations { get; set; }

    public MasterKeepInformedDbContext(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionStringProvider.GetMasterDbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEmailConfirmationEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
