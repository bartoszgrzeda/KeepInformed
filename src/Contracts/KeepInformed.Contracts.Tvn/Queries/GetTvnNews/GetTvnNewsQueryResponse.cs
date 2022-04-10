using KeepInformed.Contracts.Tvn.Dto;

namespace KeepInformed.Contracts.Tvn.Queries.GetTvnNews;

public class GetTvnNewsQueryResponse
{
    public IEnumerable<TvnNewsDto> News { get; set; }
}
