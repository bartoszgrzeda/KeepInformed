using KeepInformed.Infrastructure.BaseDbAccess.Repositories;
using KeepInformed.Domain.TenantNews.Entities;
using KeepInformed.Application.TenantNews.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.TenantDbAccess.Repositories;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(TenantKeepInformedDbContext dbContext) : base(dbContext)
    {
    }
}
