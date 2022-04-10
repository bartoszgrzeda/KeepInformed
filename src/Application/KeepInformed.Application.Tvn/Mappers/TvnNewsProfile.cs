using AutoMapper;
using KeepInformed.Contracts.Tvn.Dto;
using KeepInformed.Domain.Tvn.Entities;

namespace KeepInformed.Application.Tvn.Mappers;

public class TvnNewsProfile : Profile
{
    public TvnNewsProfile()
    {
        CreateMap<TvnNews, TvnNewsDto>()
           .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
           .ForMember(destination => destination.Title, options => options.MapFrom(source => source.TvnTitle))
           .ForMember(destination => destination.PublicationDate, options => options.MapFrom(source => source.TvnPublicationDate))
           .ForMember(destination => destination.Guid, options => options.MapFrom(source => source.TvnGuid))
           .ForMember(destination => destination.Url, options => options.MapFrom(source => source.TvnUrl))
           .ForMember(destination => destination.ImageUrl, options => options.MapFrom(source => source.TvnImageUrl))
           .ForMember(destination => destination.Description, options => options.MapFrom(source => source.TvnDescription));
    }
}
