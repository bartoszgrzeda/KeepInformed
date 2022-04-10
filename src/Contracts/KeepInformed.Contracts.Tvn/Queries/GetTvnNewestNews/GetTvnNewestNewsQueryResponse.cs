using KeepInformed.Contracts.Tvn.Dto;

namespace KeepInformed.Contracts.Tvn.Queries.GetTvnNewestNews;

public class GetTvnNewestNewsQueryResponse
{
    public IEnumerable<TvnRssItemDto> News { get; set; }
}
