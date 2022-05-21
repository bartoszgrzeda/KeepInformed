namespace KeepInformed.Common.MultiTenancy;

public interface ITenantProvider
{
    void SetUserId(Guid userId);
    Guid? GetUserId();
}
