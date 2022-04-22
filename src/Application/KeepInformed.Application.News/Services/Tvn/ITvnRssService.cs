using KeepInformed.Contracts.News.Dto.Tvn;

namespace KeepInformed.Application.News.Services.Tvn;

public interface ITvnRssService
{
    Task<IEnumerable<TvnRssItemDto>> GetNewest();
}
