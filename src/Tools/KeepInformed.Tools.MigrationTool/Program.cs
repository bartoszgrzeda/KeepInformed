using KeepInformed.Infrastructure.BaseDbAccess.ConnectionStringProvider;
using KeepInformed.Infrastructure.MasterDbAccess;
using KeepInformed.Infrastructure.MultiTenancy;
using KeepInformed.Infrastructure.TenantDbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var tenantProvider = new TenantProvider();
var connectionStringProvider = new ConnectionStringProvider(tenantProvider, configuration);

var masterContext = new MasterKeepInformedDbContext(connectionStringProvider);

Console.WriteLine($"Migrating master. ConnectionString: {connectionStringProvider.GetMasterDbConnectionString()}");
masterContext.Database.Migrate();
Console.WriteLine("Master successfully migrated");

Console.WriteLine("Migrating tenants");
foreach (var user in masterContext.Users)
{
    tenantProvider.SetUserId(user.Id);
    var tenantContext = new TenantKeepInformedDbContext(connectionStringProvider);

    Console.WriteLine($"Migrating tenant. ConnectionString: {connectionStringProvider.GetMasterDbConnectionString()}");
    tenantContext.Database.Migrate();
    Console.WriteLine("Tenant successfully migrated");
}
Console.WriteLine("Tenants succesfully migrated");