using KeepInformed.Contracts.Tvn.Dto;

namespace KeepInformed.Application.Tvn.Services;

public interface ITvnRssService
{
    Task<IEnumerable<TvnNewsDto>> GetNewest();
}
