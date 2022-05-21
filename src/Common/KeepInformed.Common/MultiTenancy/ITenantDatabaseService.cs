namespace KeepInformed.Common.MultiTenancy;

public interface ITenantDatabaseService
{
    Task CreateUserTenantDatabaseIfNotExists();
}
