using AutoMapper;
using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Contracts.Tvn.Dto;
using KeepInformed.Contracts.Tvn.Queries.GetTvnNews;
using KeepInformed.Domain.Tvn.Entities;
using MediatR;

namespace KeepInformed.Application.Tvn.Queries.GetTvnNews;

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
        var news = _tvnNewsRepository.GetAll().Select(x => _mapper.Map<TvnNews, TvnNewsDto>(x));

        return new GetTvnNewsQueryResponse()
        {
            News = news
        };
    }
}
