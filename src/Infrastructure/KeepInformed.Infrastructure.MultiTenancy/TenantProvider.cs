using KeepInformed.Common.MultiTenancy;

namespace KeepInformed.Infrastructure.MultiTenancy;

public class TenantProvider : ITenantProvider
{
    private Guid? _userId;

    public Guid? GetUserId()
    {
        return _userId;
    }

    public void SetUserId(Guid userId)
    {
        _userId = userId;
    }
}
