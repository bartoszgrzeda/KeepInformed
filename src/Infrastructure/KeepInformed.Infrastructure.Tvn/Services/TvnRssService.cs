using KeepInformed.Application.Tvn.Models;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Common;
using System.Xml.Serialization;

namespace KeepInformed.Infrastructure.Tvn.Services;

public class TvnRssService : ITvnRssService
{
    private readonly IHttpClientService _httpClientService;

    public TvnRssService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<TvnRss> GetNewest()
    {
        var url = TvnUrl.GetNewest();

        using var content = await _httpClientService.GetStreamFromUrl(url);

        var xmlSerializer = new XmlSerializer(typeof(TvnRss));
        var result = xmlSerializer.Deserialize(content) as TvnRss;

        return result;
    }
}
