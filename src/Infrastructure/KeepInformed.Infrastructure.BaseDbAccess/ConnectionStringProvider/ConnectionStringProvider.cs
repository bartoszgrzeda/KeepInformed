using KeepInformed.Common.DbAccess;
using KeepInformed.Common.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace KeepInformed.Infrastructure.BaseDbAccess.ConnectionStringProvider;

public class ConnectionStringProvider : IConnectionStringProvider
{
    private readonly ITenantProvider _tenantProvider;
    private readonly IConfiguration _configuration;

    public ConnectionStringProvider(ITenantProvider tenantProvider, IConfiguration configuration)
    {
        _tenantProvider = tenantProvider;
        _configuration = configuration;
    }

    public string GetMasterDbConnectionString()
    {
        return _configuration.GetSection("ConnectionStrings")["KeepInformed-MasterDb"];
    }

    public string GetTenantDbConnectionString()
    {
        var userId = _tenantProvider.GetUserId();
        var connectionString = _configuration.GetSection("ConnectionStrings")["KeepInformed-TenantDb"];

        return connectionString.Replace("{userId}", userId.ToString());
    }
}
