using AutoMapper;
using KeepInformed.Contracts.News.Dto;

namespace KeepInformed.Application.News.Mappers;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<Domain.News.Entities.News, NewsDto>()
           .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
           .ForMember(destination => destination.Title, options => options.MapFrom(source => source.Title))
           .ForMember(destination => destination.PublicationDate, options => options.MapFrom(source => source.PublicationDate))
           .ForMember(destination => destination.Url, options => options.MapFrom(source => source.Url))
           .ForMember(destination => destination.ImageUrl, options => options.MapFrom(source => source.ImageUrl))
           .ForMember(destination => destination.Description, options => options.MapFrom(source => source.Description))
           .ForMember(destination => destination.Seen, options => options.MapFrom(source => source.Seen))
           .ForMember(destination => destination.Source, options => options.MapFrom(source => source.Source));
    }
}
