using KeepInformed.Application.Tvn.Services;
using KeepInformed.Contracts.Tvn.Queries.GetTvnNewestNews;
using MediatR;

namespace KeepInformed.Application.Tvn.Queries.GetTvnNewestNews;

public class GetTvnNewestNewsQueryHandler : IRequestHandler<GetTvnNewestNewsQuery, GetTvnNewestNewsQueryResponse>
{
    private ITvnRssService _tvnRssService;

    public GetTvnNewestNewsQueryHandler(ITvnRssService tvnRssService)
    {
        _tvnRssService = tvnRssService;
    }

    public async Task<GetTvnNewestNewsQueryResponse> Handle(GetTvnNewestNewsQuery request, CancellationToken cancellationToken)
    {
        var result = await _tvnRssService.GetNewest();

        return new GetTvnNewestNewsQueryResponse()
        {
            News = result
        };
    }
}
