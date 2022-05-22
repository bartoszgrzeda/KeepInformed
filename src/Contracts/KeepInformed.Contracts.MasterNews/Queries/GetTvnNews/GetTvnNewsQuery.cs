using KeepInformed.Common.MediatR;

namespace KeepInformed.Contracts.MasterNews.Queries.GetTvnNews;

public class GetTvnNewsQuery : IMasterQuery<GetTvnNewsQueryResponse>
{
    public DateTime? PublicationDate { get; set; }
}
