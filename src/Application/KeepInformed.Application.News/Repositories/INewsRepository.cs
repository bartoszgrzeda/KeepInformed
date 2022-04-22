using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Contracts.News.Common;

namespace KeepInformed.Application.News.Repositories;

public interface INewsRepository : IBaseRepository<Domain.News.Entities.News>
{
    Task<Domain.News.Entities.News?> GetBySourceAndCustomStringId(NewsSource source, string customStringId);
}
