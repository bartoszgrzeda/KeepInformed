using KeepInformed.Application.MasterNews.Repositories.Tvn;
using KeepInformed.Application.MasterNews.Services.Tvn;
using KeepInformed.Contracts.MasterNews.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Domain.MasterNews.Entities.Tvn;
using MediatR;

namespace KeepInformed.Application.MasterNews.Commands.Tvn.SynchronizeTvnNewestNews;

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
            var existingNews = await _tvnNewsRepository.GetByGuid(news.Guid);

            if (existingNews == null)
            {
                var newNewsId = Guid.NewGuid();
                var newNews = new TvnNews(newNewsId, news.Title, news.Url, news.ImageUrl, news.Description, news.PublicationDate, news.Guid);

                await _tvnNewsRepository.Add(newNews);
            }

            else
            {
                existingNews.SetDescription(news.Description);
                existingNews.SetImageUrl(news.ImageUrl);
                existingNews.SetPublicationDate(news.PublicationDate);
                existingNews.SetTitle(news.Title);
                existingNews.SetUrl(news.Url);

                _tvnNewsRepository.Update(existingNews);
            }
        }

        await _tvnNewsRepository.SaveChanges();

        return Unit.Value;
    }
}
