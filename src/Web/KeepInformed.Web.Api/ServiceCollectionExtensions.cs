using KeepInformed.Application.Tvn.Queries.GetTvnNewestNews;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Services;
using MediatR;

namespace KeepInformed.Web.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetTvnNewestNewsQueryHandler)); // Tvn module

        return services;
    }

    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IHttpClientService, HttpClientService>();

        return services;
    }

    public static IServiceCollection RegisterTvn(this IServiceCollection services)
    {
        services.AddTransient<ITvnRssService, TvnRssService>();

        return services;
    }
}
