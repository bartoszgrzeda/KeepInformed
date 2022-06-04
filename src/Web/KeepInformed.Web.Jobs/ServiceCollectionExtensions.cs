using KeepInformed.Common.EventHandlers;
using KeepInformed.Contracts.Authorization.IntegrationEvents;
using KeepInformed.Web.Jobs.IntegrationEventHandlers;

namespace KeepInformed.Web.Jobs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddTransient<IIntegrationEventHandler<UserSignedUp>, SendUserEmailConfirmation>();
        services.AddTransient<IIntegrationEventHandler<UserConfirmedEmail>, CreateUserDatabase>();
        services.AddTransient<IIntegrationEventHandler<Contracts.TenantNews.IntegrationEvents.TvnNewsScheduledToBeSynchronized>, SynchronizeTvnNews>();

        return services;
    }
}
