using KeepInformed.Application.TenantNews.Repositories;
using KeepInformed.Contracts.MasterNews.Queries.GetTvnNews;
using KeepInformed.Contracts.TenantNews.Commands.SynchronizeTvnNews;
using KeepInformed.Contracts.TenantNews.Common;
using KeepInformed.Domain.TenantNews.Entities;
using MediatR;

namespace KeepInformed.Application.TenantNews.Commands.SynchronizeTvnNews;

public class SynchronizeTvnNewsCommandHandler : IRequestHandler<SynchronizeTvnNewsCommand>
{
    private readonly ISynchronizationRepository _synchronizationRepository;
    private readonly IMediator _mediator;
    private readonly INewsRepository _newsRepository;

    public SynchronizeTvnNewsCommandHandler(ISynchronizationRepository synchronizationRepository, IMediator mediator, INewsRepository newsRepository)
    {
        _synchronizationRepository = synchronizationRepository;
        _mediator = mediator;
        _newsRepository = newsRepository;
    }

    public async Task<Unit> Handle(SynchronizeTvnNewsCommand request, CancellationToken cancellationToken)
    {
        var latestSynchronization = await _synchronizationRepository.GetLatestForNewsSource(NewsSource.Tvn);

        var getTvnNewsQuery = new GetTvnNewsQuery()
        {
            PublicationDate = latestSynchronization.Date
        };
        var tvnNews = await _mediator.Send(getTvnNewsQuery);

        var news = tvnNews.News;

        if (!news.Any())
        {
            return Unit.Value;
        }

        foreach (var el in news)
        {
            var newNews = new News(Guid.NewGuid(), el.Title, el.Url, el.ImageUrl, el.Description, el.PublicationDate, NewsSource.Tvn);

            await _newsRepository.Add(newNews);
        }

        var newSynchronization = new Synchronization(Guid.NewGuid(), DateTime.UtcNow, NewsSource.Tvn, news.Count());

        await _synchronizationRepository.Add(newSynchronization);

        await _synchronizationRepository.SaveChanges();

        return Unit.Value;
    }
}
