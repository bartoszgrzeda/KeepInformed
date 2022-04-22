using KeepInformed.Application.News.Repositories;
using KeepInformed.Contracts.News.Common;
using KeepInformed.Domain.News.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(KeepInformedContext context) : base(context)
    {
    }

    public async Task<News?> GetBySourceAndCustomStringId(NewsSource source, string customStringId)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Source == source && x.CustomStringId == customStringId);
    }
}
