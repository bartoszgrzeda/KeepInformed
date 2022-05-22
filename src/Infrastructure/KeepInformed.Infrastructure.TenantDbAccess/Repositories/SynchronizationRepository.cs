using KeepInformed.Infrastructure.BaseDbAccess.Repositories;
using KeepInformed.Domain.TenantNews.Entities;
using KeepInformed.Application.TenantNews.Repositories;
using Microsoft.EntityFrameworkCore;
using KeepInformed.Contracts.TenantNews.Common;

namespace KeepInformed.Infrastructure.TenantDbAccess.Repositories;

public class SynchronizationRepository : BaseRepository<Synchronization>, ISynchronizationRepository
{
    public SynchronizationRepository(TenantKeepInformedDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Synchronization?> GetLatesForNewsSourceByLatestNewsPublicationDate(NewsSource newsSource)
    {
        return await Entities.Where(x => x.NewsSource == newsSource)
            .OrderByDescending(x => x.LatestNewsPublicationDate)
            .FirstOrDefaultAsync();
    }
}
