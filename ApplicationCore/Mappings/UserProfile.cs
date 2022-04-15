using ApplicationCore.Dtos;
using AutoMapper;
using Domain.Entity;

namespace ApplicationCore.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, LoginDto>()
                .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));

            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<RegisterDto, UserDto>().ReverseMap();
        }
    }
}