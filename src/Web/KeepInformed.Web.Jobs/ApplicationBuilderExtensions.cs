using KeepInformed.Common.EventBus;
using KeepInformed.Common.EventHandlers;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using KeepInformed.Web.Jobs.IntegrationEventHandlers;

namespace KeepInformed.Web.Jobs;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddIntegrationEventsSubscriptions(this IApplicationBuilder applicationBuilder)
    {
        var eventBus = applicationBuilder.ApplicationServices.GetRequiredService<IEventBus>();

        eventBus.Subscribe<UserSignedUp, IIntegrationEventHandler<UserSignedUp>>();
        eventBus.Subscribe<UserConfirmedEmail, IIntegrationEventHandler<UserConfirmedEmail>>();
        eventBus.Subscribe<Contracts.TenantNews.IntegrationEvents.TvnNewsScheduledToBeSynchronized, IIntegrationEventHandler<Contracts.TenantNews.IntegrationEvents.TvnNewsScheduledToBeSynchronized>>();

        return applicationBuilder;
    }
}
