using KeepInformed.Common.EventHandlers;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Contracts.TenantNews.Commands.SynchronizeTvnNews;
using MediatR;

namespace KeepInformed.Web.Jobs.IntegrationEventHandlers;

public class SynchronizeTvnNews : IIntegrationEventHandler<Contracts.TenantNews.IntegrationEvents.SynchronizeTvnNews>
{
    private readonly IMediator _mediator;
    private readonly ITenantProvider _tenantProvider;

    public SynchronizeTvnNews(IMediator mediator, ITenantProvider tenantProvider)
    {
        _mediator = mediator;
        _tenantProvider = tenantProvider;
    }

    public async Task Handle(Contracts.TenantNews.IntegrationEvents.SynchronizeTvnNews integrationEvent)
    {
        var userId = integrationEvent.UserId;

        _tenantProvider.SetUserId(userId);

        var command = new SynchronizeTvnNewsCommand();

        await _mediator.Send(command);
    }
}
