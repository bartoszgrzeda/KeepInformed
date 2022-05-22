using KeepInformed.Application.TenantNews.Repositories;
using KeepInformed.Common.Notifications;
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
    private readonly INotificationService _notificationService;

    public SynchronizeTvnNewsCommandHandler(ISynchronizationRepository synchronizationRepository, IMediator mediator, INewsRepository newsRepository, INotificationService notificationService)
    {
        _synchronizationRepository = synchronizationRepository;
        _mediator = mediator;
        _newsRepository = newsRepository;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(SynchronizeTvnNewsCommand request, CancellationToken cancellationToken)
    {
        var latestSynchronization = await _synchronizationRepository.GetLatesForNewsSourceByLatestNewsPublicationDate(NewsSource.Tvn);

        var getTvnNewsQuery = new GetTvnNewsQuery()
        {
            PublicationDate = latestSynchronization?.LatestNewsPublicationDate
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

        var latestNewsPublicationDate = news.OrderByDescending(x => x.PublicationDate)
            .First()
            .PublicationDate;
        var newSynchronization = new Synchronization(Guid.NewGuid(), NewsSource.Tvn, news.Count(), latestNewsPublicationDate);

        await _synchronizationRepository.Add(newSynchronization);

        await _synchronizationRepository.SaveChanges();

        await _notificationService.NotifyCurentUser(NotificationMethod.TVN_NEWS_SYNCHRONIZED);

        return Unit.Value;
    }
}
