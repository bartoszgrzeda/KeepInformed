using KeepInformed.Application.Tvn.Repositories;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Contracts.Tvn.Commands.SynchronizeTvnNewestNews;
using KeepInformed.Domain.Tvn.Entities;
using MediatR;

namespace KeepInformed.Application.Tvn.Commands.SynchronizeTvnNewestNews;

public class SynchronizeTvnNewestNewsCommandHandler : IRequestHandler<SynchronizeTvnNewestNewsCommand>
{
    private readonly ITvnNewsRepository _tvnNewsRepository;
    private readonly ITvnRssService _tvnRssService;

    public SynchronizeTvnNewestNewsCommandHandler(ITvnNewsRepository tvnNewsRepository, ITvnRssService tvnRssService)
    {
        _tvnNewsRepository = tvnNewsRepository;
        _tvnRssService = tvnRssService;
    }

    public async Task<Unit> Handle(SynchronizeTvnNewestNewsCommand request, CancellationToken cancellationToken)
    {
        var newestNews = await _tvnRssService.GetNewest();

        foreach (var news in newestNews)
        {
            var existingNews = await _tvnNewsRepository.GetByTvnGuid(news.Guid);

            if (existingNews == null)
            {
                var newNews = new TvnNews(news.Title, news.Url, news.ImageUrl, news.Description, news.PublicationDate, news.Guid);

                await _tvnNewsRepository.Add(newNews);
            }

            else
            {
                existingNews.SetTvnDescription(news.Description);
                existingNews.SetTvnImageUrl(news.ImageUrl);
                existingNews.SetTvnPublicationDate(news.PublicationDate);
                existingNews.SetTvnTitle(news.Title);
                existingNews.SetTvnUrl(news.Url);

                _tvnNewsRepository.Update(existingNews);
            }
        }

        await _tvnNewsRepository.SaveChanges();

        return Unit.Value;
    }
}
