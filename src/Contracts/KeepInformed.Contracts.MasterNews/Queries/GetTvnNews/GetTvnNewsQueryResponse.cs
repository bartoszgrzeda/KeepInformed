using KeepInformed.Contracts.MasterNews.Dto.Tvn;

namespace KeepInformed.Contracts.MasterNews.Queries.GetTvnNews;

public class GetTvnNewsQueryResponse
{
    public IEnumerable<TvnNewsDto> News { get; set; }
}
