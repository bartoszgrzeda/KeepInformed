using KeepInformed.Application.News.Repositories;
using KeepInformed.Application.News.Services.Tvn;
using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.News.Common;
using MediatR;

namespace KeepInformed.Application.News.Commands.Tvn.SynchronizeTvnNewestNews;

public class SynchronizeTvnNewestNewsCommandHandler : IRequestHandler<SynchronizeTvnNewestNewsCommand>
{
    private readonly INewsRepository _newsRepository;
    private readonly ITvnRssService _tvnRssService;

    public SynchronizeTvnNewestNewsCommandHandler(INewsRepository newsRepository, ITvnRssService tvnRssService)
    {
        _newsRepository = newsRepository;
        _tvnRssService = tvnRssService;
    }

    public async Task<Unit> Handle(SynchronizeTvnNewestNewsCommand request, CancellationToken cancellationToken)
    {
        var newestNews = await _tvnRssService.GetNewest();
        var newsSource = NewsSource.Tvn;

        foreach (var news in newestNews)
        {
            var existingNews = await _newsRepository.GetBySourceAndCustomStringId(newsSource, news.Guid);

            if (existingNews == null)
            {
                var newNews = new Domain.News.Entities.News(news.Title, news.Url, news.ImageUrl, news.Description, news.PublicationDate, false, newsSource, news.Guid);

                await _newsRepository.Add(newNews);
            }

            else
            {
                existingNews.SetDescription(news.Description);
                existingNews.SetImageUrl(news.ImageUrl);
                existingNews.SetPublicationDate(news.PublicationDate);
                existingNews.SetTitle(news.Title);
                existingNews.SetUrl(news.Url);

                _newsRepository.Update(existingNews);
            }
        }

        await _newsRepository.SaveChanges();

        return Unit.Value;
    }
}
