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
using KeepInformed.Web.Api.ResponseManager;
using KeepInformed.Infrastructure.MediatR.PipelineBehaviors;
using FluentValidation;
using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;

namespace KeepInformed.Web.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IResponseManager, ResponseManager.ResponseManager>();

        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IXmlDeserializer, XmlDeserializer>();

        services.AddTransient<INewsRepository, NewsRepository>();

        services.AddTransient<ITvnRssService, TvnRssService>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(typeof(GetNewsQueryHandler)); // News module

        return services;
    }

    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(MarkNewsAsSeenCommandValidator)); // News module

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TvnItemProfile)); // Tvn infrastructure
        services.AddAutoMapper(typeof(NewsProfile)); // News module

        return services;
    }

    public static IServiceCollection RegisterDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<KeepInformedContext>();

        return services;
    }
}
