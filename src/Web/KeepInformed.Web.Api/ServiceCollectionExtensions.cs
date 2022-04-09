using KeepInformed.Application.Tvn.Queries.GetTvnNewestNews;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Services;
using MediatR;
using AutoMapper;
using KeepInformed.Infrastructure.Tvn.Mapper;
using KeepInformed.Common.XmlDeserializer;

namespace KeepInformed.Web.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IXmlDeserializer, XmlDeserializer>();

        return services;
    }

    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetTvnNewestNewsQueryHandler)); // Tvn module

        return services;
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TvnItemProfile)); // Tvn infrastructure

        return services;
    }

    public static IServiceCollection RegisterTvn(this IServiceCollection services)
    {
        services.AddTransient<ITvnRssService, TvnRssService>();

        return services;
    }
}
