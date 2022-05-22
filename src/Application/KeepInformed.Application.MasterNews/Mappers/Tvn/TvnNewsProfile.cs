using AutoMapper;
using KeepInformed.Contracts.MasterNews.Dto.Tvn;
using KeepInformed.Domain.MasterNews.Entities.Tvn;

namespace KeepInformed.Application.MasterNews.Mappers.Tvn;

public class TvnNewsProfile : Profile
{
    public TvnNewsProfile()
    {
        CreateMap<TvnNews, TvnNewsDto>();
    }
}