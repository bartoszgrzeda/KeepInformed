using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Contracts.Tvn.Dto;
using KeepInformed.Contracts.Tvn.Queries.GetTvnNews;
using MediatR;

namespace KeepInformed.Application.Tvn.Queries.GetTvnNews;

public class GetTvnNewsQueryHandler : IRequestHandler<GetTvnNewsQuery, GetTvnNewsQueryResponse>
{
    private readonly ITvnNewsRepository _tvnNewsRepository;

    public GetTvnNewsQueryHandler(ITvnNewsRepository tvnNewsRepository)
    {
        _tvnNewsRepository = tvnNewsRepository;
    }

    public async Task<GetTvnNewsQueryResponse> Handle(GetTvnNewsQuery request, CancellationToken cancellationToken)
    {
        var news = _tvnNewsRepository.GetAll().Select(x => new TvnNewsDto()
        {
            Description = x.TvnDescription,
            Guid = x.TvnGuid,
            ImageUrl = x.TvnImageUrl,
            PublicationDate = x.TvnPublicationDate,
            Title = x.TvnTitle,
            Url = x.TvnUrl
        });

        return new GetTvnNewsQueryResponse()
        {
            News = news
        };
    }
}
