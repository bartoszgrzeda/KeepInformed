using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Services;
using MediatR;
using KeepInformed.Infrastructure.Tvn.Mappers;
using KeepInformed.Common.XmlDeserializer;
using KeepInformed.Infrastructure.DbAccess;
using KeepInformed.Application.News.Services.Tvn;
using KeepInformed.Application.News.Queries.GetNews;
using KeepInformed.Application.News.Repositories;
using KeepInformed.Infrastructure.DbAccess.Repositories;
using KeepInformed.Application.News.Mappers;
using KeepInformed.Infrastructure.MediatR.PipelineBehaviors;
using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Application.Authorization.Services;
using KeepInformed.Application.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Application.Authorization.Mappers;
using KeepInformed.Common.EventBus;
using KeepInformed.Infrastructure.RabbitMq;
using KeepInformed.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;
using KeepInformed.Web.Shared.ResponseManager;
using FluentValidation;
using KeepInformed.Common.MailMessage;
using KeepInformed.Infrastructure.MailMessage;

namespace KeepInformed.Web.Shared;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IResponseManager, ResponseManager.ResponseManager>();

        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IXmlDeserializer, XmlDeserializer>();

        services.AddTransient<INewsRepository, NewsRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserEmailConfirmationRepository, UserEmailConfirmationRepository>();

        services.AddTransient<ITvnRssService, TvnRssService>();

        services.AddTransient<IEncrypter, Encrypter>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        services.AddMemoryCache();

        services.AddTransient<IMailMessageBuilder, MailMessageBuilder>();
        services.AddTransient<IMailMessageSender, MailMessageSender>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddMediatR(typeof(GetNewsQueryHandler)); // News module
        services.AddMediatR(typeof(UserSignInCommandHandler)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(MarkNewsAsSeenCommandValidator)); // News module
        services.AddValidatorsFromAssemblyContaining(typeof(UserSignInCommandValidator)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TvnItemProfile)); // Tvn infrastructure
        services.AddAutoMapper(typeof(NewsProfile)); // News module
        services.AddAutoMapper(typeof(UserProfile)); // Authorization module

        return services;
    }

    public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<KeepInformedContext>();

        return services;
    }

    public static IServiceCollection RegisterRabbitMq(this IServiceCollection services)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        services.AddSingleton<IEventBus, RabbitMqEventBus>();

        return services;
    }
}
