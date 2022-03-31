using Api.Models;
using AutoMapper;
using Domain.Entity;

namespace Api.Mapper
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
            CreateMap<RegisterDto, UserEntity>();
        }
    }
}