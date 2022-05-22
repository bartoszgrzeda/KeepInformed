using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Domain.MasterNews.Entities.Tvn;

namespace KeepInformed.Application.MasterNews.Repositories.Tvn;

public interface ITvnNewsRepository : IBaseRepository<TvnNews>
{
    Task<TvnNews?> GetByGuid(string guid);
    IQueryable<TvnNews> GetByPublicationDate(DateTime publicationDate);
}
