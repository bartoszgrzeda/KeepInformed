using KeepInformed.Contracts.News.Dto;

namespace KeepInformed.Contracts.News.Queries.GetNews;

public class GetNewsQueryResponse
{
    public IEnumerable<NewsDto> News { get; set; }
}
