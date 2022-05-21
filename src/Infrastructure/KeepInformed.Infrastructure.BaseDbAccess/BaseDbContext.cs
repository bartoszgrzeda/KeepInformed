using KeepInformed.Common.DbAccess;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.BaseDbAccess;

public class BaseDbContext : DbContext
{
    protected readonly IConnectionStringProvider _connectionStringProvider;

    public BaseDbContext(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }
}
