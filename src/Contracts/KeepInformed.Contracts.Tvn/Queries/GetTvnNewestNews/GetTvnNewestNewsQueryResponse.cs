using KeepInformed.Contracts.Tvn.Dto;

namespace KeepInformed.Contracts.Tvn.Queries.GetTvnNewestNews;

public class GetTvnNewestNewsQueryResponse
{
    public IEnumerable<TvnNewsDto> News { get; set; }
}
