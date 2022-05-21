namespace KeepInformed.Common.DbAccess;

public interface IConnectionStringProvider
{
    string GetMasterDbConnectionString();
    string GetTenantDbConnectionString();
}
