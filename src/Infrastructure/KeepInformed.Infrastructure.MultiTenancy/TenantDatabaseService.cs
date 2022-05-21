using KeepInformed.Common.DbAccess;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Infrastructure.TenantDbAccess;

namespace KeepInformed.Infrastructure.MultiTenancy;

public class TenantDatabaseService : ITenantDatabaseService
{
    private readonly TenantKeepInformedDbContext _tenantContext;

    public TenantDatabaseService(TenantKeepInformedDbContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    public async Task CreateUserTenantDatabaseIfNotExists()
    {
        await _tenantContext.Database.EnsureCreatedAsync();
    }
}
