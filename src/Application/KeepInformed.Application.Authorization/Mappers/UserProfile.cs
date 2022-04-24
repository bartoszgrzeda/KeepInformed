using AutoMapper;
using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Contracts.Authorization.Dto;

namespace KeepInformed.Application.Authorization.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
           .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
           .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
           .ForMember(destination => destination.CreatedDate, options => options.MapFrom(source => source.CreatedDate))
           .ForMember(destination => destination.LastSignInDate, options => options.MapFrom(source => source.LastSignInDate));
    }
}
