using Api.Models;
using AutoMapper;
using Domain.Entity;

namespace Api.Mapper
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<UserEntity, LoginDto>()
                .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
        }
    }
}