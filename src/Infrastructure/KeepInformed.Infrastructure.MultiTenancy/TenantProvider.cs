using KeepInformed.Common.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace KeepInformed.Infrastructure.MultiTenancy;

public class TenantProvider : ITenantProvider
{
    private Guid? _userId;
    private readonly string _connectionString;

    public TenantProvider(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("ConnectionStrings")["KeepInformed-TenantDb"];
    }

    public string GetConnectionString()
    {
        return _connectionString.Replace("{userId}", _userId.ToString());
    }

    public Guid? GetUserId()
    {
        return _userId;
    }

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }
}
