using ApplicationCore.Dtos;
using AutoMapper;
using Domain.Entity;

namespace ApplicationCore.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Domain to dto
            CreateMap<UserEntity, LoginDto>()
                .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));

            //Dto to domain
            CreateMap<RegisterDto, UserEntity>();

            //Other
            CreateMap<RegisterDto, UserEntity>().ReverseMap();
        }
    }
}