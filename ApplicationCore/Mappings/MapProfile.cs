using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using AutoMapper;
using Domain.Entity;

namespace ApplicationCore.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<RefreshTokenDto, RefreshTokenEntity>().ReverseMap();
            CreateMap<RefreshTokenInputDto, RefreshTokenEntity>().ReverseMap();
            CreateMap<UserDto, LoginDto>()
                .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<RegisterDto, UserInputDto>().ReverseMap();
        }
    }
}