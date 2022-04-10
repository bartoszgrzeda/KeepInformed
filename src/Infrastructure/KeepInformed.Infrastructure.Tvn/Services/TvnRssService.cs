using KeepInformed.Infrastructure.Tvn.Models;
using KeepInformed.Application.Tvn.Services;
using KeepInformed.Common.HttpClient;
using KeepInformed.Infrastructure.Tvn.Common;
using KeepInformed.Contracts.Tvn.Dto;
using AutoMapper;
using KeepInformed.Common.XmlDeserializer;

namespace KeepInformed.Infrastructure.Tvn.Services;

public class TvnRssService : ITvnRssService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IMapper _mapper;
    private readonly IXmlDeserializer _xmlDeserializer;

    public TvnRssService(IHttpClientService httpClientService, IMapper mapper, IXmlDeserializer xmlDeserializer)
    {
        _httpClientService = httpClientService;
        _mapper = mapper;
        _xmlDeserializer = xmlDeserializer;
    }

    public async Task<IEnumerable<TvnRssItemDto>> GetNewest()
    {
        var url = TvnUrl.GetNewest();

        using var content = await _httpClientService.GetStreamFromUrl(url);
        var result = _xmlDeserializer.Deserialize<TvnRss>(content);

        return result?.Channel.Items.Select(x => _mapper.Map<TvnRssItemDto>(x)) ?? new List<TvnRssItemDto>();
    }
}
