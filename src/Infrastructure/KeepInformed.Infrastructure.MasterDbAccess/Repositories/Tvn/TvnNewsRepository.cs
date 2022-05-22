using KeepInformed.Domain.MasterNews.Entities.Tvn;
using KeepInformed.Application.MasterNews.Repositories.Tvn;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.MasterDbAccess.Repositories.Tvn;

public class TvnNewsRepository : BaseMasterRepository<TvnNews>, ITvnNewsRepository
{
    public TvnNewsRepository(MasterKeepInformedDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TvnNews?> GetByGuid(string guid)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Guid == guid);
    }

    public IQueryable<TvnNews> GetByPublicationDate(DateTime publicationDate)
    {
        return Entities.Where(x => x.PublicationDate >= publicationDate);
    }
}
