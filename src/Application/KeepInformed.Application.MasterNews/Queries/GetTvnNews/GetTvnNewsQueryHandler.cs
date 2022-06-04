using AutoMapper;
using KeepInformed.Application.MasterNews.Repositories.Tvn;
using KeepInformed.Contracts.MasterNews.Dto.Tvn;
using KeepInformed.Contracts.MasterNews.Queries.GetTvnNews;
using KeepInformed.Domain.MasterNews.Entities.Tvn;
using MediatR;

namespace KeepInformed.Application.MasterNews.Queries.GetTvnNews;

public class GetTvnNewsQueryHandler : IRequestHandler<GetTvnNewsQuery, GetTvnNewsQueryResponse>
{
    private readonly ITvnNewsRepository _tvnNewsRepository;
    private readonly IMapper _mapper;

    public GetTvnNewsQueryHandler(ITvnNewsRepository tvnNewsRepository, IMapper mapper)
    {
        _tvnNewsRepository = tvnNewsRepository;
        _mapper = mapper;
    }

    public async Task<GetTvnNewsQueryResponse> Handle(GetTvnNewsQuery request, CancellationToken cancellationToken)
    {
        var news = GetNewsQuery(request.PublicationDate)
            .ToList();

        return new GetTvnNewsQueryResponse()
        {
            News = news.Select(x => _mapper.Map<TvnNewsDto>(x))
        };
    }

    private IQueryable<TvnNews> GetNewsQuery(DateTime? publicationDate)
    {
        if (publicationDate == null)
        {
            return _tvnNewsRepository.GetAll();
        }

        else
        {
            return _tvnNewsRepository.GetByPublicationDate(publicationDate.Value);
        }
    }
}
