using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Services;
using MediatR;
using KeepInformed.Infrastructure.Tvn.Mappers;
using KeepInformed.Application.MasterNews.Services.Tvn;
using KeepInformed.Infrastructure.MediatR.PipelineBehaviors;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Application.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Application.Authorization.Mappers;
using KeepInformed.Common.EventBus;
using KeepInformed.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;
using KeepInformed.Web.Shared.ResponseManager;
using FluentValidation;
using KeepInformed.Common.MailMessage;
using KeepInformed.Infrastructure.MailMessage;
using KeepInformed.Infrastructure.MasterDbAccess.Repositories;
using KeepInformed.Infrastructure.MasterDbAccess;
using KeepInformed.Common.MultiTenancy;
using KeepInformed.Infrastructure.MultiTenancy;
using KeepInformed.Common.Encrypter;
using KeepInformed.Infrastructure.Encrypter;
using KeepInformed.Common.Jwt;
using KeepInformed.Infrastructure.Jwt;
using KeepInformed.Common.DbAccess;
using KeepInformed.Infrastructure.BaseDbAccess.ConnectionStringProvider;
using KeepInformed.Infrastructure.TenantDbAccess;
using KeepInformed.Application.MasterNews.Repositories.Tvn;
using KeepInformed.Application.MasterNews.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Infrastructure.MasterDbAccess.Repositories.Tvn;
using KeepInformed.Application.TenantNews.Repositories;
using KeepInformed.Infrastructure.TenantDbAccess.Repositories;
using KeepInformed.Application.TenantNews.Commands.SynchronizeTvnNews;
using KeepInformed.Application.MasterNews.Mappers.Tvn;
using KeepInformed.Common.Serializers;
using KeepInformed.Infrastructure.Serializers;
using KeepInformed.Common.Notifications;
using KeepInformed.Infrastructure.SignalR;
using KeepInformed.Infrastructure.ServiceBus;

namespace KeepInformed.Web.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IResponseManager, ResponseManager.ResponseManager>();

        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IXmlDeserializer, XmlDeserializer>();

        services.AddTransient<ITvnNewsRepository, TvnNewsRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserEmailConfirmationRepository, UserEmailConfirmationRepository>();
        services.AddTransient<INewsRepository, NewsRepository>();
        services.AddTransient<ISynchronizationRepository, SynchronizationRepository>();

        services.AddTransient<ITvnRssService, TvnRssService>();

        services.AddTransient<IEncrypter, Encrypter>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        services.AddMemoryCache();
        services.AddSignalR();

        services.AddTransient<IMailMessageBuilder, MailMessageBuilder>();
        services.AddTransient<IMailMessageSender, MailMessageSender>();

        services.AddTransient<ITenantDatabaseService, TenantDatabaseService>();
        services.AddTransient<INotificationService, SignalRNotificationService>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddMediatR(typeof(SynchronizeTvnNewestNewsCommandHandler)); // MasterNews module
        services.AddMediatR(typeof(UserSignInCommandHandler)); // Authorization module
        services.AddMediatR(typeof(SynchronizeTvnNewsCommandHandler)); // TenantNews module

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(UserSignInCommandValidator)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TvnItemProfile)); // Tvn infrastructure
        services.AddAutoMapper(typeof(UserProfile)); // Authorization module
        services.AddAutoMapper(typeof(TvnNewsProfile)); // MasterNews module

        return services;
    }

    public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
    {
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddTransient<IConnectionStringProvider, ConnectionStringProvider>();

        services.AddDbContext<MasterKeepInformedDbContext>();
        services.AddDbContext<TenantKeepInformedDbContext>();

        return services;
    }

    public static IServiceCollection RegisterServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<IServiceBusConnection, ServiceBusConnection>();
        services.AddSingleton<IEventBus, ServiceBusEventBus>();

        return services;
    }
}
