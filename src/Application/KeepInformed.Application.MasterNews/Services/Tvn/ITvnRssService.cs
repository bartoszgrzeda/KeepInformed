using KeepInformed.Contracts.MasterNews.Dto.Tvn;

namespace KeepInformed.Application.MasterNews.Services.Tvn;

public interface ITvnRssService
{
    Task<IEnumerable<TvnRssItemDto>> GetNewest();
}
