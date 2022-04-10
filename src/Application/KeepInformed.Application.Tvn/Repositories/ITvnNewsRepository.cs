using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Domain.Tvn.Entities;

namespace KeepInformed.Application.Tvn.Repositories;

public interface ITvnNewsRepository : IBaseRepository<TvnNews>
{
    Task<TvnNews?> GetByTvnGuid(string tvnGuid);
}
