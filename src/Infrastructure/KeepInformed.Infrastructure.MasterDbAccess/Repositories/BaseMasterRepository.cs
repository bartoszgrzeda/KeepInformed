using KeepInformed.Common.Domain.Entities;
using KeepInformed.Infrastructure.BaseDbAccess.Repositories;

namespace KeepInformed.Infrastructure.MasterDbAccess.Repositories;

public class BaseMasterRepository<TEntity> : BaseRepository<TEntity> where TEntity : BaseEntity
{
    public BaseMasterRepository(MasterKeepInformedDbContext dbContext) : base(dbContext)
    {
    }
}
