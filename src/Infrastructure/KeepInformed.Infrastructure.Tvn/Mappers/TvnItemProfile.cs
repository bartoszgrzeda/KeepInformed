using AutoMapper;
using KeepInformed.Contracts.MasterNews.Dto.Tvn;
using KeepInformed.Infrastructure.Tvn.Models;
using System.Text.RegularExpressions;

namespace KeepInformed.Infrastructure.Tvn.Mappers;

public class TvnItemProfile : Profile
{
    public TvnItemProfile()
    {
        CreateMap<TvnItem, TvnRssItemDto>()
            .ForMember(destination => destination.Title, options => options.MapFrom(source => source.Title))
            .ForMember(destination => destination.PublicationDate, options => options.MapFrom(source => source.PubDate))
            .ForMember(destination => destination.Guid, options => options.MapFrom(source => source.Guid))
            .ForMember(destination => destination.Url, options => options.MapFrom(source => source.Link))
            .ForMember(destination => destination.ImageUrl, options => options.MapFrom(source => GetImgUrl(source.Description)))
            .ForMember(destination => destination.Description, options => options.MapFrom(source => GetDescription(source.Description)));
    }

    private string GetImgUrl(string source)
    {
        var expression = "img src=\"(?<Url>.+?)\"";
        var regex = new Regex(expression);

        return regex.Match(source).Groups["Url"].Value.Trim();
    }

    private string GetDescription(string source)
    {
        var expression = ">(\\s)+(?<Description>.+)";
        var regex = new Regex(expression);

        return regex.Match(source).Groups["Description"].Value.Trim();
    }
}
