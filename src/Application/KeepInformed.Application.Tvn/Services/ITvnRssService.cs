using KeepInformed.Application.Tvn.Models;

namespace KeepInformed.Application.Tvn.Services;

public interface ITvnRssService
{
    Task<TvnRss> GetNewest();
}
