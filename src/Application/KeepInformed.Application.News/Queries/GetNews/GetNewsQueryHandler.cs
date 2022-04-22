using AutoMapper;
using KeepInformed.Application.News.Repositories;
using KeepInformed.Contracts.News.Dto;
using KeepInformed.Contracts.News.Queries.GetNews;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Application.News.Queries.GetNews;

public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, GetNewsQueryResponse>
{
    private readonly INewsRepository _newsRepository;
    private readonly IMapper _mapper;

    public GetNewsQueryHandler(INewsRepository newsRepository, IMapper mapper)
    {
        _newsRepository = newsRepository;
        _mapper = mapper;
    }

    public async Task<GetNewsQueryResponse> Handle(GetNewsQuery request, CancellationToken cancellationToken)
    {
        var tvnNewsTask = _newsRepository.GetAll().ToListAsync(cancellationToken);
        var tvnNews = await tvnNewsTask;

        var news = tvnNews.Select(x => _mapper.Map<NewsDto>(x));

        return new GetNewsQueryResponse()
        {
            News = news
        };
    }
}
